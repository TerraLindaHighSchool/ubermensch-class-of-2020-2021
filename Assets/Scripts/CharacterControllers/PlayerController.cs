using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health { get; set; }
    // What is the data type for food and oxygen? 
    public float food { get; set; }
    public float oxygen { get; set; }
    public bool isInDialogue { get; set; }
    public bool isInCombat { get; set; }
    public string nameOfNPC { get; private set; }
    InventoryManager playerInventory; 


    // Start is called before the first frame update
    void Start()
    {
        playerInventory = new InventoryManager(); 
        isInCombat = false;
        isInDialogue = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Friendly NPC"))
        {
            // set name of the npc 
            if(Input.GetKey(KeyCode.E))
            {
                isInDialogue = true; 
            }
           // Debug.Log("in conversation with friendly NPC");
        }
        if(other.CompareTag("Non-Friendly NPC"))
        {
            isInCombat = true;
           // Debug.Log("In combat with non-friendly NPC"); 
        }
        if(other.CompareTag("Inventory Object"))
        {
            if(Input.GetKey(KeyCode.E))
            {
                // make inventory item object constructs it
                InventoryItem currentItem = new InventoryItem();  
                //playerInventory.AddItem(currentItem);
              
            }
        }
    }
}