using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardPlayer : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    System.Lazy<Transform> playerTransform = new System.Lazy<Transform>(() => FindObjectOfType<PlayerProjectileLauncher>().transform);

    // Start is called before the first frame update
    void Start()
    {
        SetEnabled(false);
    }

    public void SetEnabled(bool enabled)
    {
        agent.enabled = enabled;
        if (enabled)
        {
            agent.destination = playerTransform.Value.position;
        }
    }

}
