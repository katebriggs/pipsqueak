using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] LivesPanel LivesPanel;
    [SerializeField] int maxHealth;

    public UnityEvent whenUGetDed;
    public float timeUntilEnd;
    public UnityEvent afterUGetDed;

    int remainingHealth;

    private void Start()
    {
        remainingHealth = maxHealth;
        LivesPanel.lives = remainingHealth;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F12)) {
            Ouchie();
        }
    }

    public void Ouchie()
    {
        remainingHealth--;
        LivesPanel.lives = remainingHealth;

        if (remainingHealth <= 0) {
            whenUGetDed?.Invoke();
            foreach (Enemy enemy in FindObjectsOfType<Enemy>()) {
                Destroy(enemy.gameObject);
            }
            Invoke(nameof(DoTheTHingAfterDed), timeUntilEnd);
        }
    }

    private void DoTheTHingAfterDed() {
        afterUGetDed?.Invoke();
    }
}
