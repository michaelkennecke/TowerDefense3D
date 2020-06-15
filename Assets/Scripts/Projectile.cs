using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] private const float speed = 10.0f;

    private Transform startPoint;
    private Transform target;

    void Update()
    {
        if (startPoint != null && target != null)
        {
            this.MoveToTarget();
        }
    }

    public void StartMoving(Transform startPoint, Transform target, PlayerController playerController)
    {
        this.startPoint = startPoint;
        this.target = target;
        this.playerController = playerController;
    }

    private void MoveToTarget()
    {
        Vector3 targetDirection = target.position - startPoint.position;
        float step = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            this.gameObject.SetActive(false);
            playerController.GetComponent<PlayerController>().coolDown = false;
        }   
    }
}
