using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeView : MonoBehaviour {
    public MeshRenderer mesh;

    public Material normalMat;
    public Material emptyMat;

    public YouSpinMeRightRound spin;
    public ParticleSystem onParticles;
    public ParticleSystem lossParticles;

    private bool isOn = true;

    public void SetOn(bool value) {
        if (isOn == value) return;
        isOn = value;
        mesh.sharedMaterial = value ? normalMat : emptyMat;
        spin.enabled = value;
        ParticleSystem.EmissionModule em = onParticles.emission;
        em.enabled = value;
        if (!value) lossParticles.Play();
    }
}