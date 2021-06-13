using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public CharacterController controller;
    public Animator AnimController;
    public GameObject gravityRay;
    public GameObject gravityRay1;
    public float speed { get; set; } = 4.25f;
    public float turnSpeed = 3.5f;
    int stickCount = 0; //This is for testing purposes

    private bool CanMove = true;

    private bool needsTRANS0 = true;
    private bool needsTRANS1 = true;
    private bool needsTRANSSEATTLE = true;
    private bool needsTRANSBEYOND = true;

    private void move()
    {
        //Gets inputs from the players wasd or arrow keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //calculates direction to move based on inputs
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        //Will be set to true if player can move otherwise defaults to false
        bool animate = false;

        //moves the player if move keys are pressed and CanMove is true
        if (moveDirection.magnitude >= 0.1f && CanMove)
        {
            if(needsTRANS1 && GameObject.Find("GameManager").GetComponent<TutorialController>().slidePos()[1] == 1)
            {
                Debug.Log("TRANS1");
                GameObject.Find("GameManager").GetComponent<TutorialController>().advanceSlide();
                needsTRANS1 = false;
            }

            controller.Move(moveDirection * speed * Time.deltaTime);
            Quaternion turnTo = Quaternion.Euler(0, 180 / Mathf.PI * Mathf.Atan2(horizontal, vertical), 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, turnTo, turnSpeed * Time.deltaTime);
            PreventFall(moveDirection);
            controller.Move(Vector3.down);
            animate = true;
        }
        AnimController.SetBool("isWalking", animate);
    }
    //if the player is on the ground, they do not move down. If they are off the ground, they fall down to the ground
    private void PreventFall(Vector3 moveDirection)
    {
        Physics.Raycast(gravityRay.transform.position, Vector3.down, out RaycastHit ground, controller.height);
        Physics.Raycast(gravityRay1.transform.position, Vector3.down, out RaycastHit ground1, controller.height);
        if (ground.collider == null && ground1.collider == null)
        {
            controller.Move(-moveDirection * speed * Time.deltaTime);
            CanMove = false;
            //Debug.DrawRay(gravityRay.transform.position, Vector3.down);
        }
        else
        {
            CanMove = true;
        }

    }


    //Used for testing different inventories
    public StandardInventoryItem rock;
    public StandardInventoryItem empty;
    PlayerController testHealthOxy;
   
    private bool invOpen;
    private bool bioOpen;
    private bool settingsOpen;

    private void testKeys()
    {
        HUDController TestHUDController;
        if (Input.GetKeyDown("k"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            TestHUDController.HUDLoader(0, this.gameObject, GameObject.FindGameObjectsWithTag("Friendly NPC")[0]);
        }
        if (Input.GetKeyDown("1"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            TestHUDController.HUDDeLoader(0);
        }
        if(Input.GetKeyDown("t"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            TestHUDController.HUDLoader(2, this.gameObject, GameObject.FindGameObjectsWithTag("Friendly NPC")[0]);
        }
        if (Input.GetKeyDown("3"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            TestHUDController.HUDDeLoader(2);
        }
        if(Input.GetKeyDown("q"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            TestHUDController.HUDLoader(4, this.gameObject);
        }
        if (Input.GetKeyDown("4"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            TestHUDController.HUDDeLoader(4);
        }
        if(Input.GetKeyDown("h"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            testHealthOxy = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
            testHealthOxy.health = 100;
            testHealthOxy.oxygen = 100;
        }
        if (Input.GetKeyDown("8"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            testHealthOxy = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
            testHealthOxy.health -= 17;
            testHealthOxy.oxygen -= 13;
            Debug.Log("Health: " + testHealthOxy.health);
            Debug.Log("Oxygen: " + testHealthOxy.oxygen);
        }
    } 

    //Used to open, close, and add to the player inventory
    public void inventoryOpen()
    {
        HUDController InventoryHUDController;
        if (Input.GetKeyDown(GameObject.Find("ui_settings_settingshandler").GetComponent<SettingsHandler>().inventory_Char) && !invOpen)
        {
            Debug.Log("PLAYER INSTANCE ID:" + this.gameObject.GetInstanceID());
            InventoryHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            InventoryHUDController.HUDLoader(1, this.gameObject);
            invOpen = true;
        }
        else if(Input.GetKeyDown(GameObject.Find("ui_settings_settingshandler").GetComponent<SettingsHandler>().inventory_Char) && invOpen)
        {
            InventoryHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            InventoryHUDController.HUDDeLoader(1);
            invOpen = false;
        }
        /*
        if (Input.GetKeyDown("y"))
        {
            InventoryHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            InventoryHUDController.determineInv();
            InventoryHUDController.main.AddItem(rock);
            Debug.Log("Number of items in inventory is " + GetComponentInParent<InventoryManager>().inventoryItem.Count);
            if(invOpen)
            {
                InventoryHUDController.HUDLoader();
            }
        }
        */
    }

    private void openFollowers()
    {
        HUDController playerFollowerList;
        if (Input.GetKeyDown(GameObject.Find("ui_settings_settingshandler").GetComponent<SettingsHandler>().follower_Char))
        {
            playerFollowerList = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            playerFollowerList.HUDLoader(3, this.gameObject);
        }
    }

    private void openBioMenu()
    {
        HUDController bioHUD;
        if (Input.GetKeyDown(GameObject.Find("ui_settings_settingshandler").GetComponent<SettingsHandler>().bio_Char) && !bioOpen)
        {
            bioHUD = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            bioHUD.HUDLoader(6, this.gameObject);
            bioOpen = true;
        }
        else if (Input.GetKeyDown(GameObject.Find("ui_settings_settingshandler").GetComponent<SettingsHandler>().bio_Char) && bioOpen)
        {
            bioHUD = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            bioHUD.HUDDeLoader(6);
            bioOpen = false;
        }
    }

    private bool needsTRANS13 = true;

    private void settingsMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsOpen)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().settingsHud.SetActive(true);
            settingsOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && settingsOpen)
        {
            if (needsTRANS13 && GameObject.Find("GameManager").GetComponent<TutorialController>().slidePos()[1] == 13)
            {
                GameObject.Find("GameManager").GetComponent<TutorialController>().advanceSlide();
                needsTRANS13 = false;
            }
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().settingsHud.SetActive(false);
            settingsOpen = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().inConversation) 
        {
            move(); FOR TESTING PURPOSES
        }
        */
        if(this.enabled == true)
        {
            move();
            //testKeys(); //FOR TESTING PURPOSES 
            // I added this in the git editor lamo
            inventoryOpen();
            openFollowers();
            openBioMenu();
            settingsMenu();

            if (Input.anyKeyDown)
            {
                if(needsTRANS0 && GameObject.Find("GameManager").GetComponent<TutorialController>().slidePos()[1] == 0)
                {
                    Debug.Log("TRANS0");
                    GameObject.Find("GameManager").GetComponent<TutorialController>().advanceSlide();
                    needsTRANS0 = false;
                }

                if (needsTRANSSEATTLE && GameObject.Find("GameManager").GetComponent<TutorialController>().slidePos()[0] == 1)
                {
                    Debug.Log("TRANS0");
                    GameObject.Find("GameManager").GetComponent<TutorialController>().advanceSlide();
                    needsTRANSSEATTLE = false;
                }

                if (needsTRANSBEYOND && GameObject.Find("GameManager").GetComponent<TutorialController>().slidePos()[0] == 2)
                {
                    Debug.Log("TRANS0");
                    GameObject.Find("GameManager").GetComponent<TutorialController>().advanceSlide();
                    needsTRANSBEYOND = false;
                }
            }
        }

        if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().invOpen || GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().inConversation)
        {
            CanMove = false;
        }
        else
        {
            CanMove = true;
        }

    }

    public void exitButtonMController() //used for exit button, there's like 3 methods and they're all connecte, it's confusing
    {
        HUDController InventoryHUDController;
        InventoryHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
        InventoryHUDController.HUDDeLoader(1);
        invOpen = false;
        CanMove = true;
    }

}
