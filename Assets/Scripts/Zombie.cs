using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform nexus;
    [SerializeField] private float lifePoints = 3;
    [SerializeField] GameObject explosionParticle;

    private bool running;
    private float currentLifePoints;

    public Image healthBar;

    void Start()
    {
        this.currentLifePoints = this.lifePoints;   
    }

    void Update()
    {
        animator.SetBool("IsRunning", this.running);
        this.Move();
    }

    private void Move()
    {
        this.running = true;
        this.agent.SetDestination(nexus.position);
    }

    public void Hit(int damagePoints)
    {
        this.SubtractLifePoint(damagePoints);
    }

    private void SubtractLifePoint(int damagePoints)
    {
        this.currentLifePoints -= damagePoints;
        healthBar.fillAmount = this.currentLifePoints / this.lifePoints;
        if (this.currentLifePoints <= 0)
        {
            //Destroy(this.gameObject);
            this.Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            SubtractLifePoint(1);
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity) as GameObject;
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion, 3f);
        Destroy(this.gameObject);
    }
}
