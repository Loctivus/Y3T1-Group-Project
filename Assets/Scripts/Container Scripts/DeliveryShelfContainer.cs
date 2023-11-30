using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryShelfContainer : Interactable
{
    public static DeliveryShelfContainer dscInst;
    public Crate grogCrate;
    public Crate woodCrate;

    public List<Crate> storedCrates = new List<Crate>();
    public List<int> crateFillAmounts = new List<int>();
    Animator anim;

    public TMP_Text messageText;

    void Start()
    {
        dscInst = this;
        anim = GetComponent<Animator>();
    }

    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        if (heldItem == null && storedCrates.Count > 0)
        {
            displayText = "Pick Up";
        }
        base.AvailableInteraction(heldItem, interactText);
    }

    public override void InteractedEvent()
    {
        base.InteractedEvent();

        if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] == null)
        {
            if (storedCrates.Count != 0)
            {
                var newCrate = Instantiate(storedCrates[storedCrates.Count]);
                storedCrates.RemoveAt(storedCrates.Count);
                newCrate.refillAmount = crateFillAmounts[crateFillAmounts.Count];
                crateFillAmounts.RemoveAt(crateFillAmounts.Count);
                newCrate.AddItem();
                PlayerInventory.pi.ChangeItem(newCrate);
            }
        }
    }

    public void AddCrate(string crateType, int fillAmount)
    {
        StartCoroutine(DeliveryPanelAnim());
        switch (crateType)
        {
            case "GrogCrate":
                storedCrates.Add(grogCrate);
                crateFillAmounts.Add(fillAmount);
                break;

            case "WoodCrate":
                storedCrates.Add(woodCrate);
                crateFillAmounts.Add(fillAmount);
                break;
        }
    }

    IEnumerator DeliveryPanelAnim()
    {
        messageText.text = "New delivery! Collect from the shelf by the door!";
        anim.SetTrigger("Slide In");
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("Slide Out");
    }
}
