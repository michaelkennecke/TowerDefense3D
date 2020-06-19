using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    public int damageEffect;
    public int range;
    public int projectileSpeed;

    public Projectile projectilePrefab;

    public Sprite skillSprite;
    
}
