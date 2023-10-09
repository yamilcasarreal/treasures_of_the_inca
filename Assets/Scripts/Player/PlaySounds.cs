using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public float footStepTimer;

    public float walkVolume = 0.15f;
    public float sprintVolume = 0.17f;
    public float crouchVolume = 0.10f;
    private float volume;

    public GameObject player;
    public CharacterController CC;

    private bool waiting;
    private float sprintRatio;
    private float crouchRatio;

    private void Start()
    {
        sprintRatio = (player.GetComponent<PlayerMovementTest>().sprintSpeed / player.GetComponent<PlayerMovementTest>().walkSpeed);
        crouchRatio = (player.GetComponent<PlayerMovementTest>().crouchSpeed / player.GetComponent<PlayerMovementTest>().walkSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // Only plays audio if grounded and moving a certain speed and if the previous coroutine finished
        if (CC.isGrounded && (player.GetComponent<PlayerMovementTest>().isCrouching || (player.GetComponent<PlayerMovementTest>().currentInput.x >= 2 || player.GetComponent<PlayerMovementTest>().currentInput.x <= -2) || (player.GetComponent<PlayerMovementTest>().currentInput.y >= 2 || player.GetComponent<PlayerMovementTest>().currentInput.y <= -2)))
        {
            if (!waiting)
            {
                waiting = true;
                PlaySound();
            }
        }
    }

    // Starts coroutine
    private void PlaySound()
    {
        // Check whether player is walking, sprinting, or crouching; then changes footstep timer and volume accordingly
        if (player.GetComponent<PlayerMovementTest>().isSprinting)
        {
            volume = sprintVolume;
            StartCoroutine("Timer", footStepTimer / sprintRatio);
        }
        else if (player.GetComponent<PlayerMovementTest>().isCrouching)
        {
            volume = crouchVolume;
            StartCoroutine("Timer", footStepTimer / crouchRatio);
        }
        else
        {
            volume = walkVolume;
            StartCoroutine("Timer", footStepTimer);
        }  
    }

    // Waits a certain amount of time then plays the sound
    IEnumerator Timer(float footStepTimer)
    {
        gameObject.GetComponent<FootstepSounds>().PlaySound(volume);

        yield return new WaitForSeconds(footStepTimer);

        waiting = false;
    }
}
