using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardPlayer : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    System.Lazy<Transform> playerTransform = new System.Lazy<Transform>(() => FindObjectOfType<PlayerProjectileLauncher>().transform);

    private Animator _animator;
    private static readonly int AnimParamKeyMoving = Animator.StringToHash("Moving");

    // Start is called before the first frame update
    void Start() {
        _animator = GetComponentInChildren<Animator>();
        SetEnabled(false);
    }

    private void Update() {
        _animator.SetBool(AnimParamKeyMoving, agent.velocity.sqrMagnitude > 0.01f);

        if (Vector3.SqrMagnitude(transform.position - playerTransform.Value.position) < 1)
        {
            FindObjectOfType<PlayerHealth>().Ouchie();
            Destroy(gameObject);
        }
    }

    public void SetEnabled(bool enabled)
    {
        agent.enabled = enabled;
        if (enabled) {
            agent.destination = playerTransform.Value.position;
        }
    }

}
