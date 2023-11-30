using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CupItem : Item
{
    public GameObject liquid;
    public GameObject dirt;
    public enum cupState
    {
        Empty,
        Dirty,
        Full
    };
    public cupState thisCup = cupState.Empty;

    public void Fill()
    {
        thisCup = cupState.Full;
        liquid.SetActive(true);
    }
    public void Clean()
    {
        thisCup = cupState.Empty;
        liquid.SetActive(false);
        dirt.SetActive(false);
    }
    public void Dirty()
    {
        thisCup = cupState.Dirty;
        liquid.SetActive(false);
        dirt.SetActive(true);
    }

    public bool IsDrinkable()
    {
        return thisCup == cupState.Full;
    }

    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        displayText = "";
        if (heldItem == null)
        {
            displayText = "Pick Up Cup";
        }
        base.AvailableInteraction(heldItem, interactText);
    }

}
