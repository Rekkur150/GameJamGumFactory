using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyController : MonoBehaviour
{

    private static float money = 0f;
    public float startMoney = 1000;

    private static TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        money = 0;
        AddFunds(startMoney);
    }

    private static void UpdateText()
    {
        text.text = "Money: ";
        if (money < 1000)
        {
            text.text += money.ToString();
        } 
        else if (money < 1000000f)
        {
            text.text += (money / (1000f)).ToString() + "K";
        } 
        else if (money < 1000000000f)
        {
            text.text += (money / (1000000f)).ToString() + "M";
        } 
        else if (money < 1000000000000f)
        {
            text.text += (money / (1000000000f)).ToString() + "B";
        } 
        else
        {
            text.text += (money / (1000000000000f)).ToString() + "T";
        }
    }

    public static float GetFunds()
    {
        return money;
    }

    public static bool RemoveFunds(float amount)
    {
        if ((money - amount) >= 0f)
        {
            money -= amount;
            UpdateText();
            return true;
        } 
        else
        {
            return false;
        }

    }

    public static void AddFunds(float amount)
    {
        money += amount;
        UpdateText();
    }

    public static void SetFunds(float amount)
    {
        money = amount;
        UpdateText();
    }

}
