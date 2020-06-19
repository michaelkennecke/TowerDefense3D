using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] int _hp = 5;
    [SerializeField] Explosion _explosion;
    int _currentHP;
    public float HealthRatio => (float)this._currentHP / (float)this._hp;

    [SerializeField] bool respawn;
    [SerializeField] int respawnTime;

    void Start()
    {
        this._currentHP = this._hp;
    }
    public void Hit(int damage)
    {
        this._currentHP-= damage;
        if(this._currentHP <= 0)
            this.Die();
    }

    public void Hit()
    {
        Hit(1);
    }

    public void Die()
    {
        Instantiate(this._explosion.gameObject, transform.position, Quaternion.identity);
        if (respawn == false)
        {
            Destroy(gameObject);
        }
        else
        {
            this.gameObject.SetActive(false);
            Invoke("Respawn", this.respawnTime);
        }
    }

    public void Respawn()
    {
        this._currentHP = this._hp;
        transform.position = GameObject.FindGameObjectWithTag("Nexus").transform.position;
        this.gameObject.SetActive(true);
    }
}
