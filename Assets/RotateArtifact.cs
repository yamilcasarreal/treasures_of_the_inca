using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArtifact : MonoBehaviour
{
    public float artifactHeight;
    public Vector3 rotateDirection;
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotateDirection * rotateSpeed * Time.deltaTime);
    }
}
