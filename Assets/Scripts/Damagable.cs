using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] int _hp = 5;
    [SerializeField] int _coins = 5;
    [SerializeField] Explosion _explosion;
    [SerializeField] bool _destroyObjectOnDeath = true;
    int _currentHP;
    public float HealthRatio => (float)this._currentHP / (float)this._hp;

    void Start(){
        this._currentHP = this._hp;
    }
    void OnEnable() {
        this.Start();    
    }
    public void Hit(int damage){
        
        this._currentHP = Mathf.Clamp(this._currentHP - damage, 0, this._hp);
        if(this._currentHP <= 0)
            this.Die();
    }
    public void Hit(){
        Hit(1);
    }

    public void Die(){
        Instantiate(this._explosion.gameObject, transform.position, Quaternion.identity);
        if(this._destroyObjectOnDeath){
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("SkillBar").GetComponent<SkillBarUI>().EnableUpgade();
            Money.AddMoney(this._coins);
        }else{
            gameObject.SetActive(false);
        }
        
    }
}
