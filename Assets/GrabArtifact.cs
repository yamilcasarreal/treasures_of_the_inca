using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabArtifact : MonoBehaviour, IInteract
{
    private GameObject player;

    public void Interaction()
    {
        // Finds the player gameobject
        // The "/" means the object must not have a parent
        player = GameObject.Find("/Player");

        // Runs artifact grabbed function
        player.GetComponent<ArtifactInteractions>().ArtifactGrabbed();

        // Destroys artifact
        Destroy(this.gameObject);
    }
}
