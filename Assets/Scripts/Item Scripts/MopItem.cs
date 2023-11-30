using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;

public class MopItem : Item
{
    public VisualEffect vfx;
    public Animator anim;
    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        displayText = "";
        if (heldItem == null)
        {
            displayText = "Pick Up";
        }
        base.AvailableInteraction(heldItem, interactText);
    }

    public void Sweep()
    {
        vfx.Play();
        anim.SetTrigger("Sweep");
    }

    public void EndSweep()
    {
        vfx.Stop();
    }
}
