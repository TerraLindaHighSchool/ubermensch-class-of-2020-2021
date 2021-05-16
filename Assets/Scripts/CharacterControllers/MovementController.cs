using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController controller;
    public Animator AnimController;
    public GameObject gravityRay;
    [SerializeField] public float speed = 3.5f;
    [SerializeField] public float turnSpeed = 3.5f;
    private float yVelocity;
    private const float GRAVITY = 0.4f;
    int stickCount = 0; //This is for testing purposes

    private bool CanMove = true;

    private void move()
    {
        //Gets inputs from the players wasd or arrow keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //calculates direction to move based on inputs
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        //Will be set to true if player can move otherwise defaults to false
        bool animate = false;

        //moves the player if move keys are pressed and CanMove is true
        if (moveDirection.magnitude >= 0.1f)
        {
            if (CanMove)
            {
                controller.Move(moveDirection * speed * Time.deltaTime);
                Quaternion turnTo = Quaternion.Euler(0, 180 / Mathf.PI * Mathf.Atan2(horizontal, vertical), 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, turnTo, turnSpeed * Time.deltaTime);
                //Debug.Log(turnTo);

                animate = true;
            }
        }
        AnimController.SetBool("isWalking", animate);

        //creates a Vector that keeps the player on the ground
        Vector3 moveGravity = new Vector3(0, -yVelocity * Time.deltaTime, 0);
        controller.Move(moveGravity);
    }
    //if the player is on the ground, they do not move down. If they are off the ground, they fall down to the ground
    private void setGravity()
    {
        Physics.Raycast(gravityRay.transform.position, transform.TransformDirection(Vector3.down), out RaycastHit ground, controller.height);
        if(ground.distance > .5 || ground.collider == null)
        {
            yVelocity += GRAVITY;
        }
        else
        {
            yVelocity = 0;
        }
    }

    //Used for testing different inventories
    public StandardInventoryItem rock;
    public StandardInventoryItem empty;
    private bool invOpen;
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
    } 

    //Used to open, close, and add to the player inventory
    public void inventoryOpen()
    {
        HUDController InventoryHUDController;
        if (Input.GetKeyDown("i"))
        {
            Debug.Log("PLAYER INSTANCE ID:" + this.gameObject.GetInstanceID());
            InventoryHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            InventoryHUDController.HUDLoader(1, this.gameObject);
            invOpen = true;
        }
        if (Input.GetKeyDown("2"))
        {
            InventoryHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            InventoryHUDController.HUDDeLoader(1);
            invOpen = false;
        }
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
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().inConversation) 
        {
            move(); FOR TESTING PURPOSES
        }
        */
        move();
        setGravity();
        //testKeys(); //FOR TESTING PURPOSES 
        // I added this in the git editor lamo
        inventoryOpen();
    }
}
