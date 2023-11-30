using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    public Item[] neededItem;
    [HideInInspector]
    public string displayText;

    public virtual void InteractedEvent()
    {
        CamShake.inst.ShakeCam();
        //Debug.Log("Interacted With");
        displayText = "";
        PlayerInteract.pin.HideUIPrompt();
    }

    public virtual void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        if (displayText != "")
            PlayerInteract.pin.ShowUIPrompt(displayText);
    }
}
