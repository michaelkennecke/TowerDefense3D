using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    static int score;
    static string winOrLoss;

    static Score()
    {
        score = 0;
    }

    public static void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }
    public static void LowerScore(int value)
    {
        score -= value;
    }
    public static int GetScore()
    {
        return score;
    }

    public static string GetWinOrLoss() {
        return winOrLoss;
    }

    public static void SetWinOrLoss(bool win) {
        if (win == true) {
            winOrLoss = "won";
        } else {
            winOrLoss = "lost";
        }
    }

    public static void Reset()
    {
        score = 0;
        winOrLoss = "";
    }

}