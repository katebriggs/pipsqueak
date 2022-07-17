using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPanel : MonoBehaviour {
    public int maxLives = 3;
    public int lives = 3;

    public LifeView[] lifeViews;

    private void Start()
    {
        lives = maxLives;
    }

    private void Update() {
        for (int i = 0; i < lifeViews.Length; i++) {
            lifeViews[i].SetOn(lives > i);
        }
    }
}