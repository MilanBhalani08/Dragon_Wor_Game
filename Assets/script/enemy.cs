using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public Transform player;
    public Animator animator;

    public float detectRange = 15f;     // Range to detect player and start chasing
    public float stopRange = 18f;       // Distance to stop chasing and go idle
    public float attackDistance = 3f;   // Distance to attack player

    private bool isAttacking = false;
    private bool isFollowing = false;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Attack if very close
        if (distance <= attackDistance)
        {
            agent.isStopped = true;
            animator.SetTrigger("Attack");
            isAttacking = true;
            isFollowing = false;
        }
        // Follow if in detection range
        else if (distance <= detectRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetTrigger("Run");
            isAttacking = false;
            isFollowing = true;
        }
        // Idle if player is far away
        else if (distance > stopRange && isFollowing)
        {
            agent.isStopped = true;
            animator.SetTrigger("Idle");
            isAttacking = false;
            isFollowing = false;
        }
    }
}
