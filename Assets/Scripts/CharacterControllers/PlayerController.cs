using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health { get; set; }
    public float food { get; set; }
    public float oxygen { get; set; }
    public bool isInDialogue { get; set; } //only for camera 
    private bool isInTriggerArea;
    private Collider other;
    public GameObject mainCamera; // drag main camera into this
    private GameObject objectHit; 


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
}