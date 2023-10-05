using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    public List<AudioClip> GrassClips = new List<AudioClip>();
    public List<AudioClip> WoodClips = new List<AudioClip>();
    public AudioSource audioSource;

    private int index = 0;

    public void PlaySound(float volume)
    {
        // Get's index to loop through list
        index++;
        index = index % 2;

        // Grabs the audio clip depending on which surface is below
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2.0f))
        {
            if (hit.collider.tag == "Wood")
            {
                audioSource.clip = WoodClips[index];
            }
            else // Else just play grass
            {
                audioSource.clip = GrassClips[index];
            }
        }

        // Plays the audio clip
        audioSource.volume = volume;
        audioSource.Play();
    }
}
