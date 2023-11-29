using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : MonoBehaviour
{
    System.Random rand = new System.Random();
    public Gun gun;
    public SmallEnemyAI zombie;
    [SerializeField] public int lowRange; // lowest amount of resources possibly given by any consumable
    [SerializeField] public int highRange; // highest amount of resources possibly given by any consumable

    public void GrabConsumable()
    {
        lowRange = 5;
        highRange = 11;
        int ammoGained = rand.Next(lowRange, highRange);
        gun.ammoReserve += ammoGained;
    }

    public void SpawnAmmo(Vector3 enemyPosition)
    {
        int dropAmmoCheck = rand.Next(0, 2);

        if (dropAmmoCheck == 1)
        {
            Instantiate(this.gameObject, enemyPosition, this.gameObject.transform.rotation);
        }

        else
            return;
    }
}
