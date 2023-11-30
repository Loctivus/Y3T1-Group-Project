using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WoodContainer : Interactable
{
    public Item requiredItem;
    public Crate requiredCrate;
    public int maxStorage = 15;
    public int currentStorage = 5;
    public Crate.myCrates crateType;
    public GameObject alert;
    public TMP_Text storageText;
    public WoodItem woodLog;
    
    void Start()
    {
        UpdateAmountText();
    }

    
    void Update()
    {
        
    }

    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        if (heldItem == null)
        {
            displayText = "Pick Up";
        }
        else if (heldItem.GetType() == neededItem[0].GetType())
        {
            displayText = "Use Crate";
        }


        base.AvailableInteraction(heldItem, interactText);
    }


    public override void InteractedEvent()
    {
        base.InteractedEvent();
        if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] == null && currentStorage > 0)
        {
            currentStorage--;
            var newWood = Instantiate(woodLog);
            newWood.AddItem();
            PlayerInventory.pi.ChangeItem(newWood);
            UpdateAmountText();
        }
        else if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType() == requiredCrate.GetType())
        {
            Crate heldCrate = PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].gameObject.GetComponent<Crate>();
            if (heldCrate.thisCrate == crateType)
            {
                currentStorage += heldCrate.refillAmount;
                PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].DestroyItem();
                PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] = null;
                UpdateAmountText();
            }
        }
    }

    public void UpdateAmountText()
    {
        storageText.text = currentStorage.ToString() + "/" + maxStorage.ToString();
        if (currentStorage == 0)
        {
            alert.SetActive(true);
        }
        else
        {
            alert.SetActive(false);
        }
    }
    
}
