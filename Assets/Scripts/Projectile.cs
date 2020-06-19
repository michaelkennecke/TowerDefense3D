using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int _damage = 1;
    [SerializeField] float _speed = 10f;
    
    Rigidbody _rigidbody;
    Damagable _target;
    GameObject _shooter;
    Transform _targetTransform;

    Skill _skill;

    void Awake(){
        this._rigidbody = GetComponent<Rigidbody>();
    }
    public Projectile Init(Damagable target, GameObject shooter, Skill skill){
        this._skill = skill;
        if (this._skill.damageEffect <= 0)
        {
            this._target = shooter.gameObject.GetComponent<Damagable>();
        }
        else
        {
            if (shooter.gameObject.Equals(target.gameObject))
            {
                this._target = null;
            }
            else
            {
                this._target = target;
            }
        }
        this._shooter = shooter;
        this._targetTransform = target.transform;
        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && this._shooter.CompareTag("Enemy"))
        {
            return;
        }

        Damagable enemy = other.GetComponent<Damagable>();
        if (enemy)
        {
            enemy.Hit(this._skill.damageEffect);
            Destroy(gameObject);
        }
    }

    void FixedUpdate(){
        if(this._target == null || this._target.isActiveAndEnabled == false){
            Destroy(gameObject);
            return;
        } 
        this._rigidbody.MovePosition(this._rigidbody.position + (this._targetTransform.position - this._rigidbody.position).normalized * Time.fixedDeltaTime * this._skill.projectileSpeed);
    }
}
