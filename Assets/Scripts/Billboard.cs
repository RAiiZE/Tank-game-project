using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    // reference to the camera
    public Transform cam;



    void Start()
    {// can use either to help code find the camera of the scene but bottem line is more reliable.

        // cam = GameObject.Find("CameraRig").transform;
        cam = Camera.main.transform.parent;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
