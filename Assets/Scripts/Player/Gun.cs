using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{

    //gun behaviors
    public float range = 100f;
    public bool canShoot = true, isReloading = false, isShooting = false;
    public int gunDamage, currentAmmoInGun, ammoReserve = 7, totalAmmoInGun = 7;
    public float shootDelay = 1f;
    private Recoil Recoil_Script;

    public AudioSource shooting_Sound;
    public ParticleSystem muzzle_Flash;
    public Camera fpsCam;
    public SupayAI supayAI;
    public SupayAITest supayAITest;
    public SmallEnemyAI smallEnemyAI;
    public TextMeshProUGUI ammoCount;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Recoil_Script = GetComponent<Recoil>();
        shooting_Sound = GetComponent<AudioSource>();
        supayAITest = GameObject.Find("Supay").GetComponent<SupayAITest>();
        currentAmmoInGun = 7;
        UpdateAmmoText();
        //smallEnemyAI = GameObject.FindGameObjectsWithTag("smallEnemy").GetComponent<SmallEnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmmoInGun == 0 && ammoReserve == 0)
        {
            canShoot = false;
        }

        if (canShoot && !isReloading && Input.GetButtonDown("Fire1"))
        {
            if (currentAmmoInGun == 0)
            {
                Reload();
            }

            isShooting = true;
            muzzle_Flash.Play();
            shooting_Sound.Play();
            Recoil_Script.RecoilFire();
            Shoot();
            currentAmmoInGun--;
            UpdateAmmoText();
            StartCoroutine(ShootDelay());
            isShooting = false;
            if (supayAITest.playerInSight == false)
                supayAITest.isAlerted = true;
            supayAITest.chaseTime = 20f;
        }

        if (!isShooting && Input.GetKeyDown("r"))
        {
            Reload();
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

    void Reload()
    {
        int ammoNeeded;

        ammoNeeded = totalAmmoInGun - currentAmmoInGun;

        // Player attempted to reload with a full clip or with no ammo in reserves, cancel function prematurely if so
        if (ammoNeeded == 0 || ammoReserve == 0)
            return;

        // Player reloads with sufficient ammo in their reserve to fully refill the clip
        if (ammoNeeded <= ammoReserve)
        {
            anim.SetTrigger("reload");
            StartCoroutine(ReloadDelay());
            currentAmmoInGun = 7;
            ammoReserve -= ammoNeeded;
            UpdateAmmoText();
            anim.SetTrigger("idle");
            return;
        }

        // Player reloads with ammo in their reserve, but not enough to completely reload the gun
        if (ammoNeeded > ammoReserve)
        {
            anim.SetTrigger("reload");
            StartCoroutine(ReloadDelay());
            currentAmmoInGun += ammoReserve;
            ammoReserve = 0;
            UpdateAmmoText();
            anim.SetTrigger("idle");
            return;
        }
    }

    private void UpdateAmmoText()
    {
        ammoCount.text = $"Ammo: {currentAmmoInGun}/{ammoReserve}";
    }

    IEnumerator ShootDelay()
    { //shooting timer, player can't spam shoot
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    IEnumerator ReloadDelay()
    {
        isReloading = true;
        yield return new WaitForSeconds(shootDelay);
        isReloading = false;
    }
}
