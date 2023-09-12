using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;

    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && controller.isGrounded;

    [Header("Movement Parameters")]
    public float walkingSpeed;
    public float actualSpeed;
    public float sprintSpeed;
    public float slowDownAmount;
    public float crouchSpeed = .1f;

    [Header("Jump Parameters")]
    public float jumpHeight = 5f;

    [Header("Crouch Parameters")]
    public float crouchHeight = 0.5f;
    public float standingHeight = 2f;
    public float timeToCrouch = 0.25f;
    private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;
    private bool canCrouch = true;


    [Header("Gravity Parameters")]    
    public float gravity;
    public float groundDistance;
    public LayerMask groundMask;


    [Header("Controls")]
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode crouchKey = KeyCode.LeftControl;



    public Vector3 move;
    Vector3 velocity;
    public bool isGrounded;


    public Camera playerCamera;

    public float x;
    public float z;
    

    // Update is called once per frame
    void Update()
    {
        //------------GRAVITY/GROUND CHECK---------------------------------
        // Creates sphere under player and checks if its colliding with ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Resets velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // Adds gravity
        velocity.y += gravity * 2 * Time.deltaTime;
        //-------------------------------------------------------------------


        //----------------PLAYER MOVEMENT-------------------------
        // Gets input
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

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
        // Allows player to 'sprint'
        if (Input.GetKey(sprintKey) && z > 0.2)
            actualSpeed = sprintSpeed;
        else
            actualSpeed = walkingSpeed;
        // Moves character
        controller.Move(move * actualSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        //jumping
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //crouching


        if (Input.GetKeyDown(crouchKey))
        {
            actualSpeed = crouchSpeed;
            HandleCrouch();
        }
        else
            actualSpeed = walkingSpeed;
        
        






    }

    private void HandleCrouch()
    {
        if (ShouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }
    }
    private IEnumerator CrouchStand ()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f)){
            yield break;
        }
        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = controller.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = controller.center;

        while (timeElapsed < timeToCrouch)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
        controller.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }
}
 