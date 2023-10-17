using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(AudioSource))]
public class SupaySounds : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public AudioClip[] breathingSounds;
    public AudioClip chase;
    public AudioClip jumpScare;
    public AudioClip scream;
    public AudioClip throwPlayer;
    public AudioSource screamSource;
    public AudioSource footStepSource;
    public AudioSource breathSource;
    public AudioSource chaseSource;
    public AudioSource jumpScareSource;
    //public GameObject supay;
   // public SupayAI supayAI;
    public SupayAITest supayAITest;
    // Start is called before the first frame update
    void Start()
    {
        //soundSource = GetComponent<AudioSource>();
        supayAITest = GameObject.Find("Supay").GetComponent<SupayAITest>();
        //breathSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (supayAITest.playerInSight == false)
        {
            //breathSource.Stop();
            breath();
        }
        if (supayAITest.playerInSight == true)
        {
            breathSource.Stop();
            chaseSource.clip = chase;
            if (!chaseSource.isPlaying)
                chaseSource.PlayOneShot(chaseSource.clip);
            //breathSource.Play();
        }
        if (supayAITest.playerCaptureRange == true)
        {
            chaseSource.Stop();
            jumpScareSource.clip = jumpScare;
            if (!jumpScareSource.isPlaying)
                jumpScareSource.PlayOneShot(jumpScareSource.clip);
        }
        if (supayAITest.playerCaptured == true)
        {
            jumpScareSource.Stop();
        }
    }

    public void leftFoot()
    {
        int n = Random.Range(1, footstepSounds.Length);
        footStepSource.clip = footstepSounds[n];
        footStepSource.PlayOneShot(footStepSource.clip);

        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = footStepSource.clip;

    }

    public void rightFoot()
    {
        int n = Random.Range(1, footstepSounds.Length);
        footStepSource.clip = footstepSounds[n];
        footStepSource.PlayOneShot(footStepSource.clip);

        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = footStepSource.clip;

    }

    public void breath()
    {
        int n = Random.Range(1, breathingSounds.Length);
        breathSource.clip = breathingSounds[n];
        if (!breathSource.isPlaying && supayAITest.playerInSight == false)
        {
            breathSource.PlayOneShot(breathSource.clip);
        }
        
        breathingSounds[n] = breathingSounds[0];
        breathingSounds[0] = breathSource.clip;
       

    }

    public void yell()
    {
        screamSource.clip = scream;
        screamSource.PlayOneShot(screamSource.clip);
    }

    

    


}
