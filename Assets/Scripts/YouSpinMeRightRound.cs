using System;
using UnityEngine;

public class YouSpinMeRightRound : MonoBehaviour {
    public Vector3 axis;
    public float speed;
    public Space space;

    private Quaternion startRotation;
    
    private void Start() {
        startRotation = transform.rotation;
    }

    private void Update() {
        transform.Rotate(axis, speed * Time.deltaTime, space);
    }

    private void OnDisable() {
        transform.rotation = startRotation;
    }
}