using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI _display;

    void Update()
    {
        this._display.text = $" {Money.GetMoney().ToString() + " $"}";
    }
}
