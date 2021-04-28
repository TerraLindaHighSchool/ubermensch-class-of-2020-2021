using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health { get; set; }
    public float food { get; set; }
    public float oxygen { get; set; }
    public bool isInDialogue { get; set; } //only for camera
    public GameObject mainCamera; // drag main camera into this
    private bool isInTriggerArea;
    private Collider other;
    private GameObject objectHit;

    // PlayerController Stat Additions
    // What is the default? 
    public int playerStrength;
    public int playerCharisma;
    public int playerConstitution;

    public int StatPoints;
    public int level; 

    public int npcCurrentStrength; 


    // Update is called once per frame
    void Update()
    {
        if(isInTriggerArea && Input.GetKeyDown(KeyCode.E))
        {
            Interact(); 
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            OpenMenu(); 
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
        if(isInDialogue)
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
    }

    void OpenMenu()
    {
        Debug.Log("Menu is opened"); 
    }

    public void SetPlayerStats()
    {
        // Brings down player stats
        // By how much? When does combat start? 
        if(objectHit.CompareTag("Non-Friendly NPC"))
        {            
            playerStrength --;
            playerCharisma --;
            playerConstitution --;
            Debug.Log("Is in combat, strength, charisma, and constitution declined"); 
        }

        // is the program set up that you can add NPCs yet? or is this ok?
        // adds the NPC stats to the player stats. but when? 
        playerStrength += npcStrength;
        playerCharisma += npcCharisma;
        playerConstitution += npcConstitution;
        Debug.Log("Recruited NPC stats have been added to the player's");

        // Takes away the stats if the NPC dies
        // How do you know when the NPC dies?
        playerStrength -= npcStrength;
        playerCharisma -= npcCharisma;
        playerConstitution -= npcConstitution;
        Debug.Log("Dead NPC stats have been removed from the player's");
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

    public int GetNpcStat()
    {
         
    }
}