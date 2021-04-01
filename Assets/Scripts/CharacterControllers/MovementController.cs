using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject AnimController;
    public int speed = 1;

    private bool CanMove = true;
    
    private void move()
    {
        //Gets inputs from the players wasd or arrow keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //calculates direction to move based on inputs
         Vector3 moveDirection = new Vector3(horizontal, 0f, 
            vertical).normalized;
    transform.Rotate(Vector3.up, horizontal);

        //moves the player if move keys are pressed and CanMove is true
        if (moveDirection.magnitude >= 0.1f)
        {
            if (CanMove)
            {
                controller.Move(moveDirection * speed * Time.deltaTime);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        move();
    }
}
