using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] protected ObjectPool projectilePool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private TextMeshProUGUI display;
    [SerializeField] Transform target;
    [SerializeField] float minAttackDistance = 10f;
    [SerializeField] Interactable focus;
    [SerializeField] float startHealth = 10;
    private float currentHealth;

    public bool coolDown = false;

    private bool running;
    private bool attacking;

    public Image healthBar;

    private void Start()
    {
        this.currentHealth = this.startHealth;
        this.display.text = this.currentHealth.ToString();
    }

    private void Update()
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

        if (target != null)
        {
            agent.SetDestination(target.position);
            float distance = Vector3.Distance(agent.transform.position, target.position);
            if (agent.remainingDistance <= target.GetComponent<Interactable>().radius - 1)
            {
                if (target.gameObject.CompareTag("Zombie"))
                {
                    this.Attack();
                }
            }
            else
            {
                agent.SetDestination(target.position);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    this.SetFocus(interactable);
                }
                else
                {
                    this.RemoveFocus();
                    this.Move(hit.point);
                }
            }
        }
    }

    public void Attack()
    {
        if (coolDown == false)
        {
            coolDown = true;
            Move(transform.position);
            running = false;
            attacking = true;
            StartCoroutine("waitForAttackAnimation");
        }
        return;
    }

    IEnumerator waitForAttackAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        attacking = false;
        GameObject projectile = this.projectilePool.GetPooledGameObject();
        projectile.transform.position = this.firePoint.position;
        projectile.SetActive(true);
        projectile.GetComponent<Projectile>().StartMoving(firePoint.transform, this.target.transform, this);
    }

    public void Move(Vector3 point)
    {
        if (!animator.GetBool("IsAttacking"))
        {
            this.attacking = false;
            agent.SetDestination(point);
        }
    }

    void SetFocus(Interactable newFocus)
    {
        this.focus = newFocus;
        this.FollowTarget(newFocus);
    }

    void RemoveFocus()
    {
        this.focus = null;
        this.StopFollowingTarget();
    }

    void FollowTarget(Interactable newTarget)
    {
        this.agent.stoppingDistance = newTarget.radius * 1f;
        target = newTarget.transform;
    }

    void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        target = null;
    }

    public void Hit(int damagePoints)
    {
        this.SubtractLifePoint(damagePoints);
    }

    private void SubtractLifePoint(int damagePoints)
    {
        this.currentHealth -= damagePoints;
        healthBar.fillAmount = this.currentHealth / this.startHealth;
        this.display.text = this.currentHealth.ToString();
        if (this.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
