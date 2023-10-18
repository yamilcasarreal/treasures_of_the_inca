using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteract
{
    void Interaction();
}

public class Interact : MonoBehaviour
{
    public float interactDistance;
    public SupayAITest supayAITest;

    void Start()
    {
        supayAITest = GameObject.FindGameObjectWithTag("Supay").GetComponent<SupayAITest>();
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 15;
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactDistance, layerMask))
            {
                var interactable = hit.transform.gameObject.GetComponent<IInteract>();
                supayAITest.playerInSight = true;
                supayAITest.chaseTime = 20f;
                interactable.Interaction();
            }

        }
    }
}
