using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] Controller _controller;
    public List<Skill> _shopSkills;
    [SerializeField] SkillBarUI _skillBarUI;

    public void BuySkill(int number) {
        if (this._shopSkills[number]._price <= Money.GetMoney()) {
            this._controller._skills.Add(this._shopSkills[number]);
            this._controller._startSkillDamages.Add(this._shopSkills[number]._damageEffect);
            this._skillBarUI.AddSkill(this._controller._skills.Count-1);
            Money.LowerMoney(this._shopSkills[number]._price);
            Debug.Log("Bought Skill for: " + this._controller._skills[number]._price);
        } else {
            Debug.Log("You have not enought money to buy this item");
        }
    }
}
