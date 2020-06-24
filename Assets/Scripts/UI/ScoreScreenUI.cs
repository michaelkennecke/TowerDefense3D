using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScreenUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _waveScoreUI;
    [SerializeField] TextMeshProUGUI _winOrLossUI;

    private void Update() {
        this._waveScoreUI.text = $"You reached wave {Score.GetScore().ToString()}";
        this._winOrLossUI.text = $"You {Score.GetWinOrLoss()}";
    }

    public void ToMainMenu() {
        SceneController.Instance.ToMainMenu();
    }
}
