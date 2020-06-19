using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLifeUI : MonoBehaviour
{
    [SerializeField] Nexus _nexus;
    [SerializeField] TextMeshProUGUI _numericDisplay;
    [SerializeField] Image _barDisplay;
    
    void Start(){
        this._nexus._onLifeLoss += this.Display;
        this.Display(this._nexus.PlayerLife, this._nexus.PlayerLife);
    }


    void Display(int maxlive, int currLive){
        float ratio = (float)currLive / (float)maxlive;

        this._numericDisplay.text = currLive.ToString();
        this._barDisplay.fillAmount = ratio;
    }
}
