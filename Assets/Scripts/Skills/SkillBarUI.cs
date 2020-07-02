using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : MonoBehaviour
{
    [SerializeField] SkillSlotUI _skillSlotPrefab;
    [SerializeField] Controller _controller;
    [SerializeField] LayoutGroup _layoutGroup;

    [SerializeField] SkillUpgradeUI _skillUpgradePrefab;
    [SerializeField] GameObject _skillUpgradeBar;

    List<SkillSlotUI> _skills;
    List<SkillUpgradeUI> _skillUpgrades;

    void Start(){
        this._skills = new List<SkillSlotUI>();
        this._skillUpgrades = new List<SkillUpgradeUI>();
        for (int i = 0; i < this._controller._skills.Count; i++)
        {
            this.AddSkill(i);
        }
        this.SkillChosen(1);
    }

    public void AddSkill(int i) {
        this._skills.Add(Instantiate(this._skillSlotPrefab, this._layoutGroup.transform).Init(this._controller, i+1, this._controller._skills[i]));
        this._skillUpgrades.Add(Instantiate(this._skillUpgradePrefab, this._skillUpgradeBar.GetComponent<LayoutGroup>().transform).Init(this._controller, i+1));
        this._skills[i].onExecute += this.SkillChosen;
    }

    void SkillChosen(int number){
        this._controller._skill = this._skills[number-1]._skill;
        //this._controller._audio = this._skills[number-1]._skill._audio.GetComponent<AudioSource>();
        foreach (SkillSlotUI skillSlot in this._skills)
        {
            skillSlot.Deactivate();
        }
        this._skills[number-1].Activate();
    }

    public void EnableUpgade() {
        foreach (SkillUpgradeUI skillUpgrade in this._skillUpgrades) {
            skillUpgrade.Activate();
        }
    }

    public void DisableUpgrade() {
        foreach (SkillUpgradeUI skillUpgrade in this._skillUpgrades) {
            skillUpgrade.Deactivate();
        }
    }
}
