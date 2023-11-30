using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Adventurers : TavernFolk
{
    public bool hasScroll;
    //public Transform questPoint;
    public GameObject myQuest;
    public Sprite giveScroll;
    public TMP_Text nameText;

    public override void Start()
    {
        base.Start();
        Debug.Log("Running Start");

        nameText.text = myData.name;
        if(transform.localScale.z < 0)
        {
            nameText.gameObject.transform.localScale =  new Vector3(-1 * nameText.gameObject.transform.localScale.x, nameText.gameObject.transform.localScale.y , nameText.gameObject.transform.localScale.z);
        }
    }

    public void AdventurerTookScroll()
    {
        //myQuest.transform.position = questPoint.position;
        mySeat.myContainer.heldScroll = null;
        Destroy(myQuest);
    }

    public override IEnumerator Leaving(bool satisfied)
    {

        if (satisfied)
        {
            float timeCoeff = (60f / GameTimer.inst.timeToMinute);
            float time = totalWaitTime / timeCoeff;
            pb.dangerZone = 0;
            pb.SetIcon(giveScroll);
            while (time > 0)
            {
                pb.SetProgress(time / (totalWaitTime / timeCoeff));
                time -= 1 * Time.deltaTime;
                yield return null;
            }
            pb.SetIcon(null);

            StateManager.inst.happyCustomers += 1;
            StateManager.inst.UpdateUI();
            myQuest = mySeat.CheckForScroll();
            if (myQuest != null)
            {
                AdventurerTookScroll();
                QuestController.inst.AddQuester(myData);
            }
            else
            {
                PayGold();
                TableManager.inst.AddAdventurer(myData);
            }

        }
        else
        {
            TableManager.inst.AddAdventurer(myData);
        }

        yield return base.Leaving(satisfied);
    }

    public override IEnumerator Waiting()
    {
        yield return new WaitForSeconds(3);
        if (mySeat.CheckForDrink())
        {
            mySeat.TavernFolkTookDrink();
            GivenDrink();
        }
        currentState = States.Waiting;
        float timeCoeff = (60f / GameTimer.inst.timeToMinute);
        float time = totalWaitTime / timeCoeff;
        pb.SetIcon(wantDrink);
        while (time > 0)
        {
            pb.SetProgress(time / (totalWaitTime / timeCoeff));
            time -= 1 * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(UnhappyWaiting());
    }
}
