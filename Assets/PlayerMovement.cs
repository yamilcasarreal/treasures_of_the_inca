using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform GroundCheck;

    public float walkingSpeed;
    public float sprintSpeed;
    public float slowDownAmount;
    public float gravity;
    public float groundDistance;
    public LayerMask groundMask;

    public Vector3 move;
    Vector3 velocity;
    bool isGrounded;

    public float x;
    public float z;
    float actualSpeed;

    // Update is called once per frame
    void Update()
    {
        // Gets input
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        // Creates sphere under player and checks if its colliding with ground
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        // Creates a vector with input data
        move = transform.right * x + transform.forward * z;

        // Player cant go faster when moving diagonally
        if (((x <= 1 && x > 0.5) || (x >= -1 && x < -0.5)) && ((z <= 1 && z > 0.5) || (z >= -1 && z < -0.5)))
        {
            move = Vector3.Normalize(transform.right * x + transform.forward * z);
        }

        // Slows player down once they stop moving
        if (move.magnitude < 0.5 && move.magnitude > -0.5 && move.magnitude != 0)
        {
            move *= slowDownAmount;
        }

        // Resets velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Allows player to 'sprint'
        if (Input.GetKey(KeyCode.LeftShift) && z > 0.2)
            actualSpeed = sprintSpeed;
        else
            actualSpeed = walkingSpeed;

        // Adds gravity
        velocity.y += gravity * 2 * Time.deltaTime;

        // Moves character
        controller.Move(move * actualSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}
