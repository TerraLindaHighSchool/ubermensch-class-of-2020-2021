using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject AnimController;
    public GameObject gravityRay;
    private float speed = 2.5f;
    private float turnSpeed = 3.5f;
    private float yVelocity;
    private const float GRAVITY = 0.4f;

    private bool CanMove = true;
    
    private void move()
    {
        //Gets inputs from the players wasd or arrow keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //calculates direction to move based on inputs
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        //moves the player if move keys are pressed and CanMove is true
        if (moveDirection.magnitude >= 0.1f)
        {
            if (CanMove)
            {
                controller.Move(moveDirection * speed * Time.deltaTime);
                Quaternion turnTo = Quaternion.Euler(0, 180 / Mathf.PI * Mathf.Atan2(vertical, -horizontal), 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, turnTo, turnSpeed * Time.deltaTime);
            }
        }

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

    /* tests code by pressing certain keys
    private void testKeys()
    {
        HUDController TestHUDController;
        if (Input.GetKeyDown("k"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            TestHUDController.HUDLoader(0, this.gameObject, GameObject.Find("/MockNPC"));
        }
        if (Input.GetKeyDown("l"))
        {
            TestHUDController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>();
            TestHUDController.HUDDeLoader(0);
        }
    } */
    // Update is called once per frame
    void Update()
    {
        //if (!GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().inConvo) 
        //{
            move();
        //}
        setGravity();
        //testKeys();
    }
}
