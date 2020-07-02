using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    static int money;

    static Money()
    {
        money = 0;
    }

    public static void AddMoney(int value)
    {
        money += value;
    }
    public static void LowerMoney(int value)
    {
        money -= value;
    }
    public static int GetMoney()
    {
        return money;
    }

    public static void Reset()
    {
        money = 0;
    }
}
