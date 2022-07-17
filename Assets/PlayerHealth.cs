using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] LivesPanel LivesPanel;
    [SerializeField] int maxHealth;

    int remainingHealth;

    private void Start()
    {
        remainingHealth = maxHealth;
    }

    public void Ouchie()
    {
        remainingHealth--;
        LivesPanel.lives = remainingHealth;
    }
}
