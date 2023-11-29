using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArtifact : MonoBehaviour
{
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime));
    }
}
