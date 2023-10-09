using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SupaySounds : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public AudioClip[] breathingSounds;
    public AudioClip chase;
    public AudioClip jumpScare;
    public AudioSource footStepSource;
    public AudioSource breathSource;
    public AudioSource chaseSource;
    public AudioSource jumpScareSource;
    //public GameObject supay;
    public SupayAI supayAI;
    // Start is called before the first frame update
    void Start()
    {
        //soundSource = GetComponent<AudioSource>();
        supayAI = GameObject.Find("Supay").GetComponent<SupayAI>();
        //breathSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (supayAI.isWalking == true)
        {
            //breathSource.Stop();
            breath();
        }
        if (supayAI.isChasing == true)
        {
            breathSource.Stop();
            chaseSource.clip = chase;
            if (!chaseSource.isPlaying)
                chaseSource.PlayOneShot(chaseSource.clip);
            //breathSource.Play();
        }
        if (supayAI.playerCaptured == true)
        {
            chaseSource.Stop();
            jumpScareSource.clip = jumpScare;
            if (!jumpScareSource.isPlaying)
                jumpScareSource.PlayOneShot(jumpScareSource.clip);
        }
        if (supayAI.playerThrow == true)
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
        //soundSource.Play();

        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = footStepSource.clip;

    }

    public void breath()
    {
        int n = Random.Range(1, breathingSounds.Length);
        breathSource.clip = breathingSounds[n];
        //breathSource.PlayOneShot(soundSource.clip);
        if (!breathSource.isPlaying && supayAI.isChasing == false)
        {
            breathSource.PlayOneShot(breathSource.clip);
            //breathSource.Play();
        }
        
            //breathSource.Stop();
        //StartCoroutine(wait());
        breathingSounds[n] = breathingSounds[0];
        breathingSounds[0] = breathSource.clip;
       

    }

    


}
