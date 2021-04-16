using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health { get; set; }
    public float food { get; set; }
    public float oxygen { get; set; }
    public bool isInDialogue { get; set; }
    public bool isInCombat { get; set; }
    public string nameOfNPC { get; private set; }
    InventoryManager playerInventory;
    private bool isInTriggerArea;
    private Collider other; 


    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GetComponent<InventoryManager>(); 
        isInCombat = false;
        isInDialogue = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(isInTriggerArea && Input.GetKey(KeyCode.E))
        {
            TakingAction(); 
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        isInTriggerArea = true;
        this.other = other;
        Debug.Log("entered"); 
    }

    public void OnTriggerExit(Collider other)
    {
        isInTriggerArea = false;
        this.other = null;
        isInDialogue = false;
        isInCombat = false;
        Debug.Log("Not in dialogue"); 
    }

    void TakingAction()
    {
        Debug.Log("taking action"); 
        if (other.CompareTag("Friendly NPC"))
        {
            // set name of the npc
            nameOfNPC = other.gameObject.name; 
                isInDialogue = true;
      
                Debug.Log("in conversation with " + nameOfNPC);
        }
        if (other.CompareTag("Non-Friendly NPC"))
        {
            isInCombat = true;
            // Debug.Log("In combat with non-friendly NPC"); 
        }
        if (other.CompareTag("Inventory Object"))
        {
                // make inventory item object constructs i
                // playerInventory.AddItem(other);
        }
    }
}