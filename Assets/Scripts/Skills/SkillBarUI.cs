using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : MonoBehaviour
{

    [SerializeField] GameObject skillBar;
    [SerializeField] SkillSlotUI skillSlotUIPrefab;

    [SerializeField] Controller _controller;

    SkillSlotUI[] skills;
    void Start()
    {
        int amountOfSkills = this._controller.GetSkills().Length;
        skills = new SkillSlotUI[amountOfSkills];
        for (int i = 0; i < amountOfSkills; i++)
        {
            skills[i] = Instantiate(this.skillSlotUIPrefab, this.skillBar.transform.position, Quaternion.identity).Init(this._controller, i + 1, this._controller.GetSkills()[i], this);
        }
        skills[0].GetComponent<SkillSlotUI>().SwitchColor(Color.gray);
    }

    public SkillSlotUI[] GetUISkills()
    {
        return this.skills;
    }

    public void SetDefaultColorForButtons()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].GetComponent<SkillSlotUI>().SwitchColor(Color.white);
        }
    }
}
