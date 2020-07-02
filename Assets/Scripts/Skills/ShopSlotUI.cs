using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlotUI : MonoBehaviour
{
    public Skill _skill{get;set;}
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _priceDisplay;

    int _number;

    Shop _shop;

    public ShopSlotUI Init(Shop shop,int number, Skill skill) {
        this._shop = shop;
        this._number = number;
        this._skill = skill;
        this._icon.sprite = this._skill._icon;
        this._priceDisplay.text = this._skill._price.ToString();
        return this;
    }

    public void BuySkill() {
        Debug.Log("number: " + this._number);
        this._shop.BuySkill(this._number);
    }
}
