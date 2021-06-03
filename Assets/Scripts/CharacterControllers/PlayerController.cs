using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Set health, food, and oxygen to between 0 and 100% (0.0 - 1.0)
    public float health { get; set; }
    public float food { get; set; }
    public float oxygen { get; set; }


    public bool isInDialogue { get; set; } //only for camera
    private bool isInTrading;
    public GameObject mainCamera; // drag main camera into this
    private bool isInTriggerArea;
    private Collider other;
    private GameObject objectHit;

    // Scene Attributed that affect player.  
    public float foodDepletionRate { get; set; }
    public float oxygenDepletionRate { get; set; }
    private int checkResourceLevelsEachNumSeconds;

    // PlayerController Stat Additions
    // Needs a default 
    public int playerStrength;
    public int playerCharisma;
    public int playerConstitution;

    //Total stats of NPCs following the player
    private int totalNPCStrength;
    private int totalNPCCharisma;
    private int totalNPCConstitution;

    public int statPoints; //What are stat points used for?  
    public int level;

    //AUTHOR VIVIAN***************************************************************************************************************************
    private void Awake()
    {
        health = 100;
        food = 100;
        oxygen = 100;
        checkResourceLevelsEachNumSeconds = 20;
    }

    private void Start()
    {
        // Applies scenes depletion rate once per minute of game play.
        InvokeRepeating("ConsumeResources", 2, checkResourceLevelsEachNumSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTriggerArea && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        else if(health < 0)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    void ConsumeResources()
    {
        if (oxygen > 0)
        {
            Debug.Log("oxygen at:" + oxygen);
            oxygen -= (oxygenDepletionRate);
        }
        else
        {
            InvokeRepeating("Health", 2, checkResourceLevelsEachNumSeconds / 60 + 1);
        }
    }

    void Health()
    { 
        if (health > 0)
        {
            Debug.Log("health at:" + health);
            health -= (2.5f);
        }

        if (health <= 0)
        {
            health = 0;
            GameObject UI = GameObject.FindGameObjectWithTag("UI_Manager");
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            SceneManager.LoadScene("StartMenu");
            Destroy(UI);
            foreach(GameObject e in GameObject.FindGameObjectsWithTag("DontDestroy"))
            {
                Destroy(e);
            }
            Destroy(Player);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        isInTriggerArea = true;
        objectHit = other.gameObject;
        mainCamera.GetComponent<switchCamera>().NPC = objectHit;
        Debug.Log("Entered " + objectHit.name);
    }

    public void OnTriggerExit(Collider other)
    {
        isInTriggerArea = false;
        Debug.Log("Exited");
        if (isInDialogue)
        {
            isInDialogue = false;
            GameObject.Find("GameManager").GetComponent<HUDController>().HUDDeLoader(0);
            mainCamera.GetComponent<switchCamera>().isInDialogue = false;
        }
        else if (isInTrading)
        {
            isInTrading = false;
            GameObject.Find("GameManager").GetComponent<HUDController>().HUDDeLoader(2);
        }
    }

    void Interact()
    {
        Debug.Log("taking action");
        if (objectHit.CompareTag("Friendly NPC") || objectHit.CompareTag("Non-Friendly NPC"))
        {
            if (objectHit.GetComponent<Follower>().identity.npcType != FollowerIdentity.NpcType.Merchant)
            {
                isInDialogue = true;
                GameObject.Find("GameManager").GetComponent<HUDController>().HUDLoader(0, this.gameObject, objectHit);
                mainCamera.GetComponent<switchCamera>().isInDialogue = true;
                Debug.Log("is in conversation with " + objectHit.name);
            } else
            {
                isInTrading = true;
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().HUDLoader(2, this.gameObject, objectHit);
                Debug.Log("is trading with " + objectHit.name);
            }
            
        }
        if (objectHit.CompareTag("Inventory Object"))
        {
            Debug.Log("Picking up");
            GetComponent<InventoryManager>().AddItem(objectHit.GetComponent<InventoryItemInterface>());
            objectHit.SetActive(false);
        }
        if (objectHit.CompareTag("oxygen"))
        {
            if (oxygen + objectHit.GetComponent<OxygenSupply>().oxygenSupply > 100)
            {
                oxygen = 100;
            }
            else
            {
                oxygen += objectHit.GetComponent<OxygenSupply>().oxygenSupply;
            }
            
            objectHit.SetActive(false);
        }
        if (objectHit.CompareTag("Portal"))
        {
            Debug.Log("Teleport");
            string scene = objectHit.GetComponent<PortalContainer>().portalData.Scene;
            Vector3 destination = objectHit.GetComponent<PortalContainer>().portalData.Destination;
            oxygenDepletionRate = objectHit.GetComponent<PortalContainer>().portalData.OxygenDepleteRate;
            AudioClip[] audioClips = objectHit.GetComponent<PortalContainer>().portalData.SceneMusic;
            

            int exitCheck = 0;
            foreach(InventoryItemInterface exitReq in objectHit.GetComponent<PortalContainer>().portalData.ExitRequirements)
            {
                foreach(InventoryItemInterface item in GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().equipMenu.PrintInventory())
                {
                    if(item == exitReq) { exitCheck++; }
                }
            }
            if(exitCheck >= objectHit.GetComponent<PortalContainer>().portalData.ExitRequirements.Length)
            {
                //Sets MusicController's "tracks" field to the music put into the scriptable object portal
                GameObject.Find("Main Camera").GetComponent<MusicController>().TrackSwitch(0, audioClips);
                GetComponentInParent<TransitionController>().SceneLoader(scene, destination);
            }
        }
        if(objectHit.CompareTag("HomeBase"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().HUDLoader(4, this.gameObject);
        }
    }

    //AUTHOR NICHOLAS**********************************************************************************************************

    // Getter methods that return the player stats: 
    public int GetPlayerStrength()
    {
        AddFollowerStatsToPlayer();
        return playerStrength + totalNPCStrength;
    }

    public int GetPlayerCharisma()
    {
        AddFollowerStatsToPlayer();
        return playerCharisma + totalNPCCharisma;
    }

    public int GetPlayerConstitution()
    {
        AddFollowerStatsToPlayer();
        return playerConstitution + totalNPCConstitution;
    }

    //Sets the level number and increases the stat points
    // Will be called when player moves up a level
    public void SetLevelNum()
    {
        level++;
        statPoints += 2;
    }

    public void AddFollowerStatsToPlayer()
    {
        FollowerManager fm = GetComponent<FollowerManager>(); // gets the follower manager 
        FollowerIdentity[] followers = fm.PrintFollowers();
        totalNPCStrength = 0;
        totalNPCCharisma = 0;
        totalNPCConstitution = 0;
        foreach (FollowerIdentity f in followers)
        {
            // gets follow identity of the follower in the array 
            FollowerIdentity currentFollower = f;
            // gets the individual stat 
            int followerStrength = currentFollower.GetFollowerStrength();
            int followerCharisma = currentFollower.GetFollowerCharisma();
            int followerConstitution = currentFollower.GetFollowerConstitution();
            // adds the follower's stat to the player's stat

            totalNPCStrength += followerStrength;
            totalNPCCharisma += followerCharisma;
            totalNPCConstitution += followerConstitution;
            Debug.Log("Follower Stats Totaled");
        }
    }
    public void exitTrade() //to be used by the exit button in UI
    {
        isInTrading = false;
        GameObject.Find("GameManager").GetComponent<HUDController>().HUDDeLoader(2);
        this.GetComponent<MovementController>().exitButtonMController();
    }
}