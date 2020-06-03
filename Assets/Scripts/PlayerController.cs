using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    private bool running;
    private bool attacking;

    void Update()
    {
        animator.SetBool("IsRunning", this.running);
        animator.SetBool("IsAttacking", this.attacking);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            running = false;
        }
        else
        {
            running = true;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            Move(transform.position);
            running = false;
            attacking = true;
        }
    }

    public void Move(Vector3 point)
    {
        this.attacking = false;
        agent.SetDestination(point);
    }
}
