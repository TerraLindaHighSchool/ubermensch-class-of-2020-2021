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

    // PlayerController Stat Additions
    // Needs a default 
    public int playerStrength;
    public int playerCharisma;
    public int playerConstitution;

    public int statPoints; //What are stat points used for?  
    public int level;

    private void Awake()
    {
        health = 100;
        food = 100;
        oxygen = 100;
    }

    private void Start()
    {
        // Applies scenes depletion rate once per minute of game play.
        InvokeRepeating("ConsumeResources", 2, 60);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTriggerArea && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void ConsumeResources()
    {
        if (oxygen > 0)
        {
            Debug.Log("oxygen at:" + oxygen);
            oxygen -= (oxygenDepletionRate);
        }
        else if (health > 0)
        {
            Debug.Log("health at:" + health);
            health -= (33f);
        }

        if (health < 0)
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
            if (objectHit.GetComponent<Follower>().identity.merchant == false)
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
        if (objectHit.CompareTag("Portal"))
        {
            Debug.Log("Teleport");
            string scene = objectHit.GetComponent<PortalContainer>().portalData.Scene;
            Vector3 destination = objectHit.GetComponent<PortalContainer>().portalData.Destination;
            oxygenDepletionRate = objectHit.GetComponent<PortalContainer>().portalData.OxygenDepleteRate;
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
                GetComponentInParent<TransitionController>().SceneLoader(scene, destination);
            }
        }
    }

    public void SetPlayerStats()
    {
        AddFollowerStatsToPlayer();
        RemoveFollowerStatsFromPlayer();

        if (isInTriggerArea == true && objectHit.CompareTag("Non-Friendly NPC"))
        {
            playerStrength--;
            playerCharisma--;
            playerConstitution--;
            Debug.Log("Is in combat, strength, charisma, and constitution declined");
        }
    }

    // Getter methods that return the player stats: 
    public int GetPlayerStrength()
    {
        return playerStrength;
    }

    public int GetPlayerCharisma()
    {
        return playerCharisma;
    }

    public int GetPlayerConstitution()
    {
        return playerConstitution;
    }

    //Sets the level number and increases the stat points
    // Will be called when player moves up a level
    public void SetLevelNum()
    {
        level++;
        playerStrength += 2;
        playerCharisma += 2;
        playerConstitution += 2;
    }

    public void AddFollowerStatsToPlayer()
    {
        FollowerManager fm = GetComponent<FollowerManager>(); // gets the follower manager 
        FollowerIdentity[] followers = fm.PrintFollowers();
        foreach (FollowerIdentity f in followers)
        {
            // gets follow identity of the follower in the array 
            FollowerIdentity currentFollower = f;
            // gets the individual stat 
            int followerStrength = currentFollower.GetFollowerStrength();
            int followerCharisma = currentFollower.GetFollowerCharisma();
            int followerConstitution = currentFollower.GetFollowerConstitution();
            // adds the follower's stat to the player's stat
            playerStrength += followerStrength;
            playerCharisma += followerCharisma;
            playerConstitution += followerConstitution;
            Debug.Log("Follower Stats Added");
        }
    }

    public void RemoveFollowerStatsFromPlayer()
    {
        FollowerManager fm = GetComponent<FollowerManager>(); // gets the follower manager 
        FollowerIdentity[] followers = fm.PrintFollowers();
        foreach (FollowerIdentity f in followers)
        {
            /*
            FollowerManager currentFollowerManager = GetComponent<FollowerManager>();
            if (currentFollowerManager.followerRemoved == true)
            {
                FollowerIdentity currentFollower = GetComponent<FollowerIdentity>();
                // gets the individual stat 
                int followerStrength = currentFollower.GetFollowerStrength();
                int followerCharisma = currentFollower.GetFollowerCharisma();
                int followerConstitution = currentFollower.GetFollowerConstitution();
                // removes the stat from the player's 
                playerStrength -= followerStrength;
                playerCharisma -= followerCharisma;
                playerConstitution -= followerConstitution;
                Debug.Log("Dead Follower Stats removed");
            }
            */
        }
    }
    public void exitTrade() //to be used by the exit button in UI
    {
        isInTrading = false;
        GameObject.Find("GameManager").GetComponent<HUDController>().HUDDeLoader(2);
        this.GetComponent<MovementController>().exitButtonMController();
    }
}