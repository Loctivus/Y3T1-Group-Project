using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crate : Item
{
    public int refillAmount;
    public TMP_Text fillAmount;

    private void Start()
    {
        setFillText();
    }

    public enum myCrates
    {
        WoodCrate,
        GrogCrate
    };
    public myCrates thisCrate = myCrates.WoodCrate;

    public void setFillText()
    {
        fillAmount.text = refillAmount.ToString();
    }

    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        displayText = "";
        if (heldItem == null)
        {
            displayText = "Pick Up Crate";
        }
        base.AvailableInteraction(heldItem, interactText);
    }
}
