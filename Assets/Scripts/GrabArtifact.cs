using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabArtifact : MonoBehaviour, IInteract
{
    private GameObject player;
    public SupayAITest supayAITest;

    void Start()
    {
        supayAITest = GameObject.FindGameObjectWithTag("Supay").GetComponent<SupayAITest>();
    }

    public void Interaction()
    {
        // Finds the player gameobject
        // The "/" means the object must not have a parent
        player = GameObject.Find("/Player");

        // Runs artifact grabbed function
        player.GetComponent<ArtifactInteractions>().ArtifactGrabbed();
        supayAITest.playerInSight = true;
        supayAITest.chaseTime = 20f;

        // Destroys artifact
        Destroy(this.gameObject);
    }
}
