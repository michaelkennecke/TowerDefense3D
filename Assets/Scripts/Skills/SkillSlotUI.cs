using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    // FFFF00

    private Controller _controller;
    private int _skillId;
    private Skill _skill;
    private SkillBarUI _skillBarUI;
    private Transform _transform;

    public SkillSlotUI Init(Controller controller, int skillId, Skill skill, SkillBarUI skillBarUI)
    {
        this._controller = controller;
        this._skillId = skillId;
        this._skill = skill;
        this._skillBarUI = skillBarUI;
        this.transform.SetParent(skillBarUI.gameObject.transform, false);
        this.GetComponent<Button>().GetComponent<Image>().sprite = skill.skillSprite;
        return this;
    }

    public void SwichtSkill()
    {
        this._skillBarUI.SetDefaultColorForButtons();
        this._controller.SwitchSkill(this._skillId);
        this.SwitchColor(Color.gray);
    }

    public void ClickSkillButton()
    {
        this.gameObject.GetComponent<Button>().onClick.Invoke();
    }

    public void SwitchColor(Color color)
    {
        ColorBlock colors = this.GetComponent<Button>().colors;
        colors.normalColor = color;
        this.GetComponent<Button>().colors = colors;
    }
}
