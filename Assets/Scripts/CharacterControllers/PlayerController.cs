using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Set health, food, and oxygen to between 0 and 100% (0.0 - 1.0)
    public float health { get; set; }
    public float food { get; set; }
    public float oxygen { get; set; }


    public bool isInDialogue { get; set; } //only for camera
    public GameObject mainCamera; // drag main camera into this
    private bool isInTriggerArea;
    private Collider other;
    private GameObject objectHit;

    // Scene Attributed that affect player.  
    public float oxygenDepletionRate { get; set; }

    // PlayerController Stat Additions
    // Needs a default 
    public int playerStrength;
    public int playerCharisma;
    public int playerConstitution;

    public int statPoints; //What are stat points used for?  
    public int level;
    private void Start()
    {
        // Applies scenes depletion rate once per minute of game play.
        InvokeRepeating(nameof(ConsumeResources), 0, 60);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTriggerArea && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenMenu();
        }
    }

    void ConsumeResources()
    {
        // food consumption goes here

        if (oxygen > 0)
        {
            oxygen -= oxygenDepletionRate;
        }
        else if (health > 0)
        {
            health -= 0.33f;
        }

        if (health < 0)
        {
            health = 0;
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
    }

    void Interact()
    {
        Debug.Log("taking action");
        if (objectHit.CompareTag("Friendly NPC") || objectHit.CompareTag("Non-Friendly NPC"))
        {
            isInDialogue = true;
            GameObject.Find("GameManager").GetComponent<HUDController>().HUDLoader(0, this.gameObject, objectHit);
            mainCamera.GetComponent<switchCamera>().isInDialogue = true;
            Debug.Log("is in conversation with " + objectHit.name);
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
            OxygenConsumptionToDepletionRate();
            GetComponentInParent<TransitionController>().SceneLoader(scene, destination);

        }
    }

    void OpenMenu()
    {
        Debug.Log("Menu is opened");
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
        ArrayList followers = fm.followers;
        foreach (Follower f in followers)
        {
            // gets follow identity of the follower in the array 
            FollowerIdentity currentFollower = GetComponent<FollowerIdentity>();
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
        ArrayList followers = fm.followers;
        foreach (Follower f in followers)
        {
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
        }
    }

    private void OxygenConsumptionToDepletionRate()
    {
        float oxygenConsumption = objectHit.GetComponent<PortalContainer>().portalData.MinutesToConsumeOxygen;
        if (oxygenConsumption != 0)
        {
            oxygenDepletionRate = 1 / oxygenConsumption;
        }
    }
}