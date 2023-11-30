using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : TavernFolk
{

    public override IEnumerator Leaving(bool satisfied)
    {
        if (satisfied) 
        {
            PayGold();
            StateManager.inst.happyCustomers += 1;
            StateManager.inst.UpdateUI();
        }
        if (hasDrink)
            mySeat.PutDownCup();

        return base.Leaving(satisfied);
    }


    public override IEnumerator Waiting()
    {
        if (mySeat.CheckForDrink())
        {
            mySeat.TavernFolkTookDrink();
            GivenDrink();
        }


        currentState = States.Waiting;
        float timeCoeff = (60f / GameTimer.inst.timeToMinute);
        float angryTime = totalWaitTime / timeCoeff;
        pb.SetIcon(wantDrink);
        while (angryTime > 0)
        {
            pb.SetProgress(angryTime / (totalWaitTime / timeCoeff));
            angryTime -= 1 * Time.deltaTime;
            yield return null;
        }
        pb.SetIcon(null);
        StartCoroutine(UnhappyWaiting());
    }
}
