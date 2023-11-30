using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat: MonoBehaviour
{
    public Transform seatPos;
    public bool inUse = false;
    public TavernFolk folkInSeat;

    public SeatContainer myContainer;

    public bool CheckForDrink()
    {
        if(myContainer.heldDrink != null)
        {
            if (myContainer.heldDrink.GetComponent<CupItem>().IsDrinkable())
            {
                return true;
            }
            
        }
        return false;
    }

    public GameObject CheckForScroll()
    {
        if (myContainer.heldScroll != null)
        {
            Debug.Log("Has Scroll");
            var scrollData = myContainer.heldScroll.GetComponent<ScrollItem>();
            if (scrollData != null)
            {
                QuestController.inst.OrderQuest(scrollData.myData);
            }

            return myContainer.heldScroll.gameObject;
        }
        else
        {
            Debug.Log("Has No Scroll");
            return null;
        }
    }

    public void TavernFolkTookScroll()
    {

        //myContainer.canUseContainer = false;
        myContainer.heldScroll.cl.enabled = false;
    }




    public bool CheckForAdventurer()
    {
        if (folkInSeat.myData != null)
        {
            Debug.Log("Has adventurer data");
            return true;
        }
        else
        {
            Debug.Log("Has No adventurer data");
            return false;
        }
    }

    public void TavernFolkTookDrink()
    {
        
        //myContainer.canUseContainer = false;
        myContainer.heldDrink.cl.enabled = false;
    }

    public void CanGiveDrink()
    { 
        if (folkInSeat != null)
        {
            if (CheckForDrink())
            {
                TavernFolkTookDrink();
                folkInSeat.GivenDrink();
            }
        }

        
    }

    public void PickUpCup(Transform handPos)
    {
        myContainer.heldDrink.gameObject.transform.position = handPos.position;
    }

    public void PutDownCup()
    {
        myContainer.heldDrink.gameObject.transform.position = myContainer.slot1OnTable.position;
        myContainer.heldDrink.GetComponent<CupItem>().Dirty();
        //myContainer.canUseContainer = true;
    }
}
