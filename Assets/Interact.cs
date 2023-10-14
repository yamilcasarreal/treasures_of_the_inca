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
                interactable.Interaction();
            }

        }
    }
}
