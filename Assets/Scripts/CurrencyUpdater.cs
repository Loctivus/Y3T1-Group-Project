using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUpdater : MonoBehaviour
{
    public TMP_Text currencyText;
    public TMP_Text currencyChangedText;
    public int currentBalance;
    public int newBalance;
    float changeSpeed = 0.25f;
    
    public void IncreaseBalance(int amount)
    {
        if (amount > 25)
        {
            changeSpeed = 0.01f;
        }
        else
        {
            changeSpeed = 0.25f;
        }
        currencyChangedText.text = "+" + amount;
        newBalance = currentBalance + amount;
        StartCoroutine(UpdateCurrencyUI());
    }

    public void ReduceBalance(int amount)
    {
        if (amount > 25)
        {
            changeSpeed = 0.01f;
        }
        else
        {
            changeSpeed = 0.25f;
        }
        currencyChangedText.text = "-" + amount;
        newBalance = currentBalance - amount;
        StartCoroutine(UpdateCurrencyUI());
    }

    public IEnumerator UpdateCurrencyUI()
    {
        if (currentBalance != newBalance)
        {
            if (newBalance < currentBalance)
            {
                currentBalance--;
                
            }
            else if (newBalance > currentBalance)
            {
                currentBalance++;
            }

            currencyText.text = currentBalance.ToString();
            yield return new WaitForSeconds(changeSpeed);
            StartCoroutine(UpdateCurrencyUI());
        }
        else
        {
            currencyChangedText.text = "";
        }


        
    }

}
