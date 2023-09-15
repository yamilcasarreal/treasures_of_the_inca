using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public float footStepTimer;
    public GameObject player;

    private bool waiting;

    // Update is called once per frame
    void Update()
    {
        // Only plays audio if moving a certain speed and if the previous coroutine finished
        if ((player.GetComponent<PlayerMovementTest>().currentInput.x >= 2 || player.GetComponent<PlayerMovementTest>().currentInput.x <= -2) || (player.GetComponent<PlayerMovementTest>().currentInput.y >= 2 || player.GetComponent<PlayerMovementTest>().currentInput.y <= -2))
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
        StartCoroutine("Timer", footStepTimer);
    }

    // Waits a certain amount of time then plays the sound
    IEnumerator Timer(float footStepTimer)
    {
        gameObject.GetComponent<FootstepSounds>().PlaySound();

        yield return new WaitForSeconds(footStepTimer);

        waiting = false;
    }
}
