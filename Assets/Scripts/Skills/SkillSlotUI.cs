using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlotUI : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] Image _chosenFrame;
    [SerializeField] TextMeshProUGUI _numberDisplay;
    int _number;
    Controller _controller;
    public Skill _skill{get;set;}
    public delegate void OnExecute(int number);
    public OnExecute onExecute;

    public SkillSlotUI Init(Controller controller, int number, Skill skill){
        this._controller = controller;
        this._number = number;
        this._numberDisplay.text = this._number.ToString();
        this._skill = skill;
        this._icon.sprite = this._skill._icon;
        return this;
    }

    public void OnClick(){
        if(this.onExecute != null)
            this.onExecute.Invoke(this._number);
    }

    public void Activate(){
        this._chosenFrame.gameObject.SetActive(true);
    }
    public void Deactivate(){
        this._chosenFrame.gameObject.SetActive(false);
    }

    void Update(){
        if(Input.GetKeyDown(this._number.ToString()))
            this.OnClick();
    }

}
