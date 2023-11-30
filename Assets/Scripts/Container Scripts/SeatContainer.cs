using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeatContainer : Interactable
{
    //public Item[] requiredItems;
    public Transform slot1OnTable;
    public Transform slot2OnTable;
    public Item heldDrink;
    public Item heldScroll;
    //public bool canUseContainer;
    public Seat mySeat;
    public Table myTable;


    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        displayText = "";
        if (heldItem == null)
        {
            if (heldDrink != null)
                displayText = "Pick Up";
            if (heldScroll != null)
                displayText = "Pick Up";
        }
        else if (heldItem.GetType() == neededItem[0].GetType())
        {
            if (heldItem.GetComponent<CupItem>().thisCup == CupItem.cupState.Full)
            {
                displayText = "Place Drink";
            }
        }
        else if (heldItem.GetType() == neededItem[1].GetType())
        {
            displayText = "Place Scroll";
        }
        else if (heldItem.GetType() == neededItem[2].GetType())
        {
            displayText = "Clean Spill";
        }
        
        


        base.AvailableInteraction(heldItem, interactText);
    }

    public override void InteractedEvent()
    {
        base.InteractedEvent();

        if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] == null)
        {
            
            if (heldDrink != null && mySeat.inUse == false)
            {
                
                heldDrink.AddItem();
                PlayerInventory.pi.ChangeItem(heldDrink);
                heldDrink = null;
                Debug.Log("Held drink on seat should now be empty");
                return;
            }

            if (heldScroll != null && mySeat.inUse == false)
            {
               
                Debug.Log("Picked up scroll from empty seat");
                heldScroll.AddItem();
                PlayerInventory.pi.ChangeItem(heldScroll);
                heldScroll = null;
                
                return;
            }
        }
        else if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType() == neededItem[0].GetType())
        {
            if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetComponent<CupItem>().thisCup == CupItem.cupState.Full)
            {
                if (heldDrink == null)
                {
                    heldDrink = PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem];
                    PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].DropItem();
                    PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] = null;
                    PlayerInteract.pin.objectsInRange.Remove(heldDrink);
                    heldDrink.transform.position = slot1OnTable.position;
                    heldDrink.cl.enabled = false;
                    mySeat.CanGiveDrink();
                }
            }
        }
        else if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType() == neededItem[1].GetType())
        {
            if (heldScroll == null)
            {
                heldScroll = PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem];
                PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].DropItem();
                PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] = null;
                PlayerInteract.pin.objectsInRange.Remove(heldScroll);
                heldScroll.transform.position = slot2OnTable.position;
                heldScroll.cl.enabled = false;
            }
        }
        else if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType() == neededItem[2].GetType())
        {
            PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetComponent<MopItem>().Sweep();
            myTable.CleanSpill();
        }
        
    }
}
