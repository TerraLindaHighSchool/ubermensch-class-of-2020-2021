using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Set health, food, and oxygen to between 0 and 100% (0.0 - 100.0)
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
    public string yourName = "J. Doe";
    public Sprite profilePic;
    public string currentMission = "Get to the Arc of Life and Save Humanity";

    //AUTHOR VIVIAN***************************************************************************************************************************
    private void Awake()
    {
        health = 100;
        food = 100;
        oxygen = 10;
    }

    private void Start()
    {
        string[] names =
            {
            "Taylor, D.",
            "Drew R.", 
            "Jordan .C",
            "Ryan, E.", 
            "Charlie F.", 
            "Haven, Z.", 
            "Morgan, G", 
            "Aiden, E.", 
            "Mason. L", 
            "Patrick O.",
            "Matt V.", 
            "Luna S."
            };

        yourName = names[Random.Range(0, 12)];

        GameObject.Find("GameManager").GetComponent<TutorialController>().tutorialLoader(0);
        // Applies scenes depletion rate once per minute of game play.
        InvokeRepeating("ConsumeResources", 2, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTriggerArea && Input.GetKeyDown(GameObject.Find("ui_settings_settingshandler").GetComponent<SettingsHandler>().interact_Char))
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
            bool wearingMask = false;

            Debug.Log("oxygen at:" + oxygen);

            foreach (InventoryItemInterface i in GameObject.Find("GameManager").GetComponent<HUDController>().equipMenu.inventoryItem)
            {
                if (i.Name == "Gas Mask")
                {
                    wearingMask = true;
                }
            }

            if(wearingMask)
            {
                oxygen -= (oxygenDepletionRate * 0.5f);
            }
            else
            {
                oxygen -= (oxygenDepletionRate);
            }
        }
        else
        {
            oxygen = 0;
            InvokeRepeating("Health", 2, 1);
        }
    }

    void Health()
    {
        if (health > 0)
        {
            if(oxygen == 0)
            {

                Debug.Log("Health: " + health);
                Debug.Log("Constitution" + GetPlayerConstitution());
                health -= 2.5f * ((11.0f - GetPlayerConstitution())/10.0f); //This line is cause the health not to decline at all.
            }
            else
            {
                if(health >= 100)
                {
                    CancelInvoke("Health");
                }
                health += 2.5f;// + GetPlayerConstitution();
            }
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

    private bool needsTRANS2 = true;

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

        if (objectHit.CompareTag("Message") || objectHit.CompareTag("Map"))
        {
            isInDialogue = true;
            GameObject.Find("GameManager").GetComponent<HUDController>().HUDLoader(0, this.gameObject, objectHit);
            Debug.Log("Reading " + objectHit.name);
        }

        if (objectHit.CompareTag("Inventory Object"))
        {
            Debug.Log("Picking up");
            GetComponent<InventoryManager>().AddItem(objectHit.GetComponent<InventoryItemInterface>());
            objectHit.SetActive(false);
        }

        if (objectHit.CompareTag("oxygen"))
        {
            if(needsTRANS2 && GameObject.Find("GameManager").GetComponent<TutorialController>().slidePos()[1] == 2)
            {
                GameObject.Find("GameManager").GetComponent<TutorialController>().advanceSlide();
                needsTRANS2 = false;
            }

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
            GameObject.Find("GameManager").GetComponent<TutorialController>().endShow();
            string scene = objectHit.GetComponent<PortalContainer>().portalData.Scene;
            Debug.Log("Teleport to: " + scene);
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

        int bonus = 0;

        foreach (InventoryItemInterface i in GameObject.Find("GameManager").GetComponent<HUDController>().equipMenu.inventoryItem)
        {
            bonus += i.StrengthBoost;
        }

        return playerStrength + totalNPCStrength + bonus;
    }

    public int GetPlayerCharisma()
    {
        AddFollowerStatsToPlayer();

        int bonus = 0;

        foreach (InventoryItemInterface i in GameObject.Find("GameManager").GetComponent<HUDController>().equipMenu.inventoryItem)
        {
            bonus += i.CharismaBoost;
        }

        return playerCharisma + totalNPCCharisma + bonus;
    }

    public int GetPlayerConstitution()
    {
        AddFollowerStatsToPlayer();

        int bonus = 0;

        foreach (InventoryItemInterface i in GameObject.Find("GameManager").GetComponent<HUDController>().equipMenu.inventoryItem)
        {
            bonus += i.ConstitutionBoost;
        }

        return playerConstitution + bonus;
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