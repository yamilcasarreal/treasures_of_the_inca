using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    //gun behaviors
    public float range = 100f;
    public bool canShoot = true;
    public float shootDelay = 1f;
    private Recoil Recoil_Script;

    public AudioSource shooting_Sound;
    public ParticleSystem muzzle_Flash;
    public Camera fpsCam;
    public SupayAI supayAI;
    public SupayAITest supayAITest;

    

    
    // Start is called before the first frame update
    void Start()
    {
        Recoil_Script = GetComponent<Recoil>();
        shooting_Sound = GetComponent<AudioSource>();
        supayAITest = GameObject.Find("Supay").GetComponent<SupayAITest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            muzzle_Flash.Play();
            shooting_Sound.Play();
            Recoil_Script.RecoilFire();
            Shoot();
            StartCoroutine(ShootDelay());
            //supayAI.isChasing = true;
        }
    }
    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.tag == "Supay")
            {
                supayAITest.playerInSight = true;
                supayAITest.gotShot = true;
                supayAITest.reset = true;
            }
            Debug.Log(hit.transform.name);
        }
    }

    IEnumerator ShootDelay()
    { //shooting timer, player can't spam shoot
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;

    }
}
