using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUpgradeUI : MonoBehaviour
{
    int _number;
    Controller _controller;

    public SkillUpgradeUI Init(Controller controller, int number){
        this._controller = controller;
        this._number = number;
        this.Deactivate();
        return this;
    }

    public void UpgradeSkill() {
        Debug.Log("Skill " + this._number + " upgraded!");
        if (this._controller._skills[this._number-1]._damageEffect >= 0) {
            this._controller._skills[this._number-1]._damageEffect += 1;
        } else {
            this._controller._skills[this._number-1]._damageEffect -= 1;
        }
        GameObject.FindGameObjectWithTag("SkillBar").GetComponent<SkillBarUI>().DisableUpgrade();
    }

    public void Activate(){
        this.gameObject.SetActive(true);
    }
    public void Deactivate(){
        this.gameObject.SetActive(false);
    }

}
