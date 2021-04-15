using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health { get; set; }
    // What is the data type for food and oxygen? 
    public bool food { get; set; }
    public bool oxygen { get; set; }
    public bool isInDialogue { get; private set; }
    public bool isInCombat { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        // what should the initail values be? 
        health = 10;
        food = true;
        oxygen = true; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Friendly NPC"))
        {
            isInDialogue = true; 
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
                // pick up and put in inventory
                // how to call the add to inventory method in this class 
            }
        }
    }
}
