using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sink : Interactable
{
    public static Sink inst;

    public Item requiredItem;
    public Item heldItem;

    public int maxStorage = 1;
    public int currentStorage = 0;
    public List<Item> cleanedCups = new List<Item>();

    public List<Transform> cleanSpots = new List<Transform>();
    public Transform cleanSpot;
    public Transform dirtySpot;

    bool accesible = false;


    public ProgressBar myProgBar;

    void Start()
    {
        inst = this;
    }

    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        if (heldItem == null)
        {
            displayText = "Pick Up";
        }
        else if (heldItem.GetType() == neededItem[0].GetType())
        {
            displayText = "Clean Cup";
        }


        base.AvailableInteraction(heldItem, interactText);
    }

    public override void InteractedEvent()
    {
        base.InteractedEvent();
        if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] == null)
        {
            if(heldItem != null && accesible)
            {
                //heldItem.AddItem();
                //PlayerInventory.pi.ChangeItem(heldItem);
                //heldItem = null;
                
                for (int i = 0; i < cleanedCups.Count; i++)
                {
                    if (cleanedCups[i] != null)
                    {
                        cleanedCups[i].AddItem();
                        PlayerInventory.pi.ChangeItem(cleanedCups[i]);
                        cleanedCups[i] = null;
                        break;
                    }
                }
                currentStorage--;
            }
        }
        else if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType() == requiredItem.GetType())
        {
            if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetComponent<CupItem>().thisCup == CupItem.cupState.Dirty)
            {
                if (currentStorage < maxStorage)
                {
                    currentStorage += 1;
                    heldItem = PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem];
                    PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].DropItem();
                    PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] = null;
                    heldItem.cl.enabled = false;
                    PlayerInteract.pin.objectsInRange.Remove(heldItem);
                    //heldItem.transform.parent = dirtySpot;
                    heldItem.transform.position = dirtySpot.position;
                    StartCoroutine(CleanUp());
                    StartCoroutine(UI());
                }
            }
        }
    }

    public int GetFreeSpot()
    {
        for (int i = 0; i < cleanSpots.Count; i++)
        {
            if (cleanedCups[i] == null)
            {
                return i;
            }
            
        }
        return -1;
    }

    public void UpgradeCleanedCapacity(string s, int n)
    {
        if (s == "Cleaned Cup Space")
        {
            maxStorage += n;
        }
                
    }

    IEnumerator CleanUp()
    {
        accesible = false;       
        yield return new WaitForSeconds(600 / (60f / GameTimer.inst.timeToMinute));
        //heldItem.transform.position = cleanSpot.position;
        int i = GetFreeSpot();
        cleanedCups[i] = heldItem;
        //cleanedCups.Add(heldItem);
        heldItem.transform.position = cleanSpots[i].position;
        heldItem.GetComponent<CupItem>().Clean();
        accesible = true;
    }

    IEnumerator UI()
    {
        float timeCoeff = (60f / GameTimer.inst.timeToMinute);
        float t = 0;
        while(t <= 600/timeCoeff)
        {
            t += 1 * Time.deltaTime;
            myProgBar.SetProgress(t/(600/timeCoeff));
            yield return null;
        }
        myProgBar.SetProgress(0);
        yield return null;
    }
}
