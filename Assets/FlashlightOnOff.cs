using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightOnOff : MonoBehaviour
{

    public GameObject flashlight;
    public AudioSource audioSource;

    // Turns flashlight on/off if "F" is pressed
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight.activeSelf)
            {
                flashlight.SetActive(false);
                audioSource.Play();
            }
            else
            {
                flashlight.SetActive(true);
                audioSource.Play();
            }
        }
    }
}