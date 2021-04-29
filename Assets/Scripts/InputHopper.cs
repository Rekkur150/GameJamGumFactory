using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHopper : MonoBehaviour
{
    public int AmountHolding = 0;
    public int MaxAmountHolding = 1000;

    public static float baseCost = 10f;
    public float colorMultiplier = 2f;
    public float sizeMultiplier = 16f;

    public bool HoldInfinite = true;

    public bool IsApartOfMachine = false;
    public bool GetMoneyForGum = false;
    public bool CheckForContract = false;

    public Contract[] Contracts;

    public MachineController machineController;


    public bool AddHolding(GameObject obj)
    {
        if (IsApartOfMachine)
        {
            machineController.AddObject(obj);

            return false;
        } else
        {

            if (GetMoneyForGum)
            {
                Color temp = obj.GetComponent<Renderer>().material.color;

                float price = baseCost;

                float totalColorMultiplier = 0f;
                totalColorMultiplier += (temp.r / (0.25f)) * colorMultiplier;
                totalColorMultiplier += (temp.g / (0.25f)) * colorMultiplier;
                totalColorMultiplier += (temp.b / (0.25f)) * colorMultiplier;

                float totalSizeMultiplier = 0f;
                totalSizeMultiplier += (obj.GetComponent<RubberController>().timesScaled) * sizeMultiplier;

                if (totalColorMultiplier != 0)
                {
                    price = price * Mathf.Round(totalColorMultiplier);
                }
                
                if (totalSizeMultiplier != 0)
                {
                    price = price * Mathf.Round(totalSizeMultiplier);
                }

                MoneyController.AddFunds(Mathf.Round(price));
            }

            if (CheckForContract)
            {
                for (int i = 0; i < Contracts.Length; i++)
                {
                    if (Contracts[i] != null)
                    {
                        Contracts[i].CheckObject(obj);
                    }
                }
            }

            if (!HoldInfinite || AmountHolding + 1 > MaxAmountHolding)
            {
                return false;
            }
            else
            {
                AmountHolding++;
                return true;
            }
        }
    }
}
