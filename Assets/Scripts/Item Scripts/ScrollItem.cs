using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollItem : Item
{
    public QuestButton myData;

    public override void InteractedEvent()
    {
        base.InteractedEvent();
        //QuestManager.qMInst.AddQuest(myData.daysToComplete, myData.reward);
        
    }

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
