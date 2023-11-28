using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    
    public AudioSource footStepSource;
    public AudioClip chase;
    public AudioSource chaseSource;
    public AudioClip attackSound;
    public AudioSource attackSoundSource;
    public AudioClip hit;
    public AudioSource hitSource;
    public SmallEnemyAI smallEnemyAI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (smallEnemyAI.isStaggered)
        {
            GotHit();
        }
    }
    public void ZombieLeftFoot()
    {
        int n = Random.Range(1, footstepSounds.Length);
        footStepSource.clip = footstepSounds[n];
        footStepSource.PlayOneShot(footStepSource.clip);

        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = footStepSource.clip;

    }
    public void ZombieRightFoot()
    {
        int n = Random.Range(1, footstepSounds.Length);
        footStepSource.clip = footstepSounds[n];
        footStepSource.PlayOneShot(footStepSource.clip);

        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = footStepSource.clip;

    }
    public void ZombieAttack()
    {
        attackSoundSource.PlayOneShot(attackSound);
    }
    public void GotHit()
    {
        hitSource.PlayOneShot(hitSource.clip);
    }
}
