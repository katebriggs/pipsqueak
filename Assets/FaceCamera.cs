using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    System.Lazy<Camera> mainCam = new System.Lazy<Camera>(() => Camera.main);

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.Value.transform.position, Vector3.up);
    }
}
