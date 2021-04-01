using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject AnimController;
    public int speed = 1;
    public int turnSpeed = 5;
    private bool CanMove = true;
    
    private void move()
    {
        //Gets inputs from the players wasd or arrow keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //calculates direction to move based on inputs
        Vector3 moveDirection = (new Vector3(horizontal, 0f, vertical)).normalized;

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
    }
    // Update is called once per frame
    void Update()
    {
        move();
    }
}
