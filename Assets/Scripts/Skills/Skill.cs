using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill")]
public class Skill : ScriptableObject
{
    public int _damageEffect = 1;
    public float _range = 10f;
    public float _projectileSpeed = 10f;
    public Projectile _projectilePrefab;
    public Sprite _icon;

    public AudioSource _audio;
    
    //Not needed in task ;) the higher the attackSpeed, the faster the Animator plays -> we cann Shoot more often in the Controller
    [Range(0.1f,10f)] public float _attackSpeed = 1f;

    public void Execute(Controller caster, Vector3 projectileSpawnPosition, Damagable target){
        Instantiate(this._projectilePrefab, projectileSpawnPosition, Quaternion.identity).Init(target, this._damageEffect, this._projectileSpeed);
    }
}
