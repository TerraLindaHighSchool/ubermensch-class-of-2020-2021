﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    public CharacterController controller;
    public Animator AnimController;
    public GameObject gravityRay;
    public GameObject gravityRay1;
    [SerializeField] public float speed = 3.5f;
    [SerializeField] public float turnSpeed = 3.5f;
    int stickCount = 0; //This is for testing purposes

    private bool CanMove = true;

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
        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveDirection * speed * Time.deltaTime);
            Quaternion turnTo = Quaternion.Euler(0, 180 / Mathf.PI * Mathf.Atan2(horizontal, vertical), 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, turnTo, turnSpeed * Time.deltaTime);
            PreventFall(moveDirection);
            controller.Move(Vector3.down);

            if(CanMove)
            { 
                animate = true;
            }
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
        if (Input.GetKeyDown("i") && !invOpen)
        {
            Debug.Log("PLAYER INSTANCE ID:" + this.gameObject.GetInstanceID());
            InventoryHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            InventoryHUDController.HUDLoader(1, this.gameObject);
            invOpen = true;
        }
        else if(Input.GetKeyDown("i") && invOpen)
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
        if (Input.GetKeyDown("u"))
        {
            playerFollowerList = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            playerFollowerList.HUDLoader(3, this.gameObject);
        }
    }

    private void openBioMenu()
    {
        HUDController bioHUD;
        if (Input.GetKeyDown("o") && !bioOpen)
        {
            bioHUD = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            bioHUD.HUDLoader(6, this.gameObject);
            bioOpen = true;
        }
        else if (Input.GetKeyDown("o") && bioOpen)
        {
            bioHUD = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            bioHUD.HUDDeLoader(6);
            bioOpen = false;
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
