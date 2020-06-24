using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int _damage = 1;
    float _speed = 10f;
    Rigidbody _rigidbody;
    Damagable _target;
    Transform _targetTransform;

    void Awake(){
        this._rigidbody = GetComponent<Rigidbody>();
    }
    public Projectile Init(Damagable target, int damage = 1, float speed = 10f){
        this._target = target;
        this._targetTransform = target.transform;
        this._damage = damage;
        this._speed = speed;
        return this;
    }

    private void OnTriggerEnter(Collider other) {
        Damagable damagable = other.GetComponent<Damagable>();
        if(damagable && damagable == this._target){
            damagable.Hit(this._damage);
            Destroy(gameObject);
        }
    }


    void FixedUpdate(){
        if(!this._target || !this._target.isActiveAndEnabled){
            Destroy(gameObject);
            return;
        } 
        this._rigidbody.MovePosition(this._rigidbody.position + (this._targetTransform.position - this._rigidbody.position).normalized * Time.fixedDeltaTime * this._speed);
    }
}
