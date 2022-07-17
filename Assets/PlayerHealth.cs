using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] LivesPanel LivesPanel;
    [SerializeField] int maxHealth;

    public UnityEvent whenUGetDed;

    int remainingHealth;

    private void Start()
    {
        remainingHealth = maxHealth;
    }

    public void Ouchie()
    {
        remainingHealth--;
        LivesPanel.lives = remainingHealth;
        whenUGetDed?.Invoke();
    }
}
