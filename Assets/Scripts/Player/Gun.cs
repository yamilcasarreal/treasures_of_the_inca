using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    //gun behaviors
    public float range = 100f;
    public bool canShoot = true;
    public int gunDamage;
    public float shootDelay = 1f;
    private Recoil Recoil_Script;
    public Animator anim;
    public AudioSource shooting_Sound;
    public ParticleSystem muzzle_Flash;
    public Camera fpsCam;
    public SupayAI supayAI;
    public SupayAITest supayAITest;
    public SmallEnemyAI smallEnemyAI;

    

    
    // Start is called before the first frame update
    void Start()
    {
        Recoil_Script = GetComponent<Recoil>();
        shooting_Sound = GetComponent<AudioSource>();
        supayAITest = GameObject.Find("Supay").GetComponent<SupayAITest>();
        //smallEnemyAI = GameObject.FindGameObjectsWithTag("smallEnemy").GetComponent<SmallEnemyAI>();
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
            if (supayAITest.playerInSight == false)
                supayAITest.isAlerted = true;
            supayAITest.chaseTime = 20f;
        }
        if (Input.GetKeyDown("r")){
            anim.SetTrigger("Reload");
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
                //supayAITest.chaseTime = 10f;
                supayAITest.isStaggered = true;
            }

            if (hit.collider.gameObject.tag == "smallEnemy")
            {
                smallEnemyAI = hit.collider.gameObject.GetComponent<SmallEnemyAI>();
                hit.collider.gameObject.GetComponent<SmallEnemyHP>().changeSmallEnemyHP(-gunDamage);
                smallEnemyAI.playerInSight = true;
                //supayAITest.chaseTime = 10f;
                smallEnemyAI.isStaggered = true;
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
