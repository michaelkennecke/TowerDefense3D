using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ShopSlotUI _shopSlotUIPrefab;
    [SerializeField] Controller _controller;
    [SerializeField] Shop _shop;
    [SerializeField] LayoutGroup _layoutGroup;
    List<ShopSlotUI> _skills;

    int _countHelper;

    void Start() {
        //this._countHelper = this._shop._shopSkills.Count + this._controller._skills.Count;
        this._skills = new List<ShopSlotUI>();
        for (int i = 0; i < this._shop._shopSkills.Count; i++ ) {
            this._skills.Add(Instantiate(this._shopSlotUIPrefab, this._layoutGroup.transform).Init(this._shop, i, this._shop._shopSkills[i]));
        }
    }
}
