using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform startPoint;
    Transform target;
    Transform _transform;
    private const float speed = 1.0f;

    public virtual Bullet Init(Transform startPoint, Transform target)
    {
        this.startPoint = startPoint;
        this.target = target;
        return this;
    }

    private void Start()
    {
        _transform = transform;    
    }

    void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        float fracJourney = Time.time * speed;
        this.transform.position = Vector3.Lerp(this.startPoint.position, this.target.position, fracJourney);
    }
}
