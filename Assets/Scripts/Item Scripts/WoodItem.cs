using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WoodItem : Item
{
    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        displayText = "";
        if (heldItem == null)
        {
            displayText = "Pick Up";
        }
        base.AvailableInteraction(heldItem, interactText);
    }
}
