using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GrogContainer : Interactable
{
    public int maxStorage = 10;
    public int currentStorage = 5;

    public Item requiredItem;
    public Crate requiredCrate;
    public Crate.myCrates crateType;

    public CupItem cupItem;
    public CupItem[] storedCups;
    public GameObject[] cupObjects;

    public bool[] validSpace;
    public GameObject alert;
    public TMP_Text amountText;

    List<GameObject> cups = new List<GameObject>();

    void Start()
    {
        GameStateEvents.inst.OnPlayerDied += ClearAllCups;
        GameStateEvents.inst.OnGameInit += SetContainer;
        
    }

    void ClearAllCups()
    {
        maxStorage = 10;
        currentStorage = 5;
        foreach (GameObject cup in cups)
        {
            Destroy(cup);
        }
    }

    void SetContainer()
    {
        UpdateAmountText();
        for (int i = 0; i < cupObjects.Length; i++)
        {
            if (validSpace[i])
            {
                var newCup = Instantiate(cupItem);
                newCup.transform.position = cupObjects[i].transform.position;
                storedCups[i] = newCup;
                validSpace[i] = false;

            }
        }
    }

    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        if (heldItem == null)
        {
            displayText = "Pick Up";

        }
        else if (heldItem.GetType() == neededItem[0].GetType())
        {
            displayText = "Store Cup";

        }

        base.AvailableInteraction(heldItem, interactText);
    }

    public override void InteractedEvent()
    {
        base.InteractedEvent();
        if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] == null && currentStorage > 0)
        {
            for (int i = 0; i < validSpace.Length; i++)
            {
                if (!validSpace[i])
                {
                    currentStorage -= 1;
                    storedCups[i].AddItem();
                    storedCups[i].Fill();
                    PlayerInventory.pi.ChangeItem(storedCups[i]);
                    cups.Add(storedCups[i].gameObject);
                    storedCups[i] = null;
                    validSpace[i] = true;
                    UpdateAmountText();
                    break;
                }
            }
        }
        else if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType() == requiredCrate.GetType())
        {
            Crate heldCrate = PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].gameObject.GetComponent<Crate>();
            if (heldCrate.thisCrate  == crateType)
            {
                if (currentStorage < maxStorage - 2)
                {
                    currentStorage += heldCrate.refillAmount;
                    PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].DestroyItem();
                    PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] = null;
                    UpdateAmountText();
                }  
            }
        }
        else if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType() == requiredItem.GetType())
        {
            if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetComponent<CupItem>().thisCup == CupItem.cupState.Empty)
            {
                for (int i = 0; i < validSpace.Length; i++)
                {
                    if (validSpace[i])
                    {
                        storedCups[i] = PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetComponent<CupItem>();
                        PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].DropItem();
                        PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] = null;
                        storedCups[i].cl.enabled = false;
                        PlayerInteract.pin.objectsInRange.Remove(storedCups[i]);
                        storedCups[i].transform.position = cupObjects[i].transform.position;
                        validSpace[i] = false;
                        break;
                    }
                }
            }
        }
    }



    public void UpdateAmountText()
    {
        amountText.text = "Drinks:" + currentStorage + "/" + maxStorage;
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
