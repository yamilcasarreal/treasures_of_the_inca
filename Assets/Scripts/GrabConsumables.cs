using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabConsumables : MonoBehaviour, IInteract
{
    private GameObject player;

    public void Interaction()
    {
        // Finds the player gameobject
        // The "/" means the object must not have a parent
        player = GameObject.Find("/Player");

        // Runs ammo grabbed function
        player.GetComponent<Consumables>().GrabConsumable();

        // Destroys ammo
        Destroy(this.gameObject);
    }
}
