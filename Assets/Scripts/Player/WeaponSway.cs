using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour{


    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        //calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRoatation = rotationX * rotationY;

        //rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRoatation, smooth * Time.deltaTime);



    }
}
