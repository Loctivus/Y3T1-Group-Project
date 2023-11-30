using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class FirePlace : Interactable
{
    public Item requiredItem;
    public VisualEffect fx;
    public bool angry;
    public ProgressBar pb;
    public static FirePlace inst;
    float angerDelay;
    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        GameStateEvents.inst.OnStartDay += Begin;
    }
    void Begin()
    {
        StopAllCoroutines();
        angry = false;
        fx.Stop();
        StartCoroutine(LifeCycle());
    }
    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        displayText = "";
        if (heldItem == null)
        {
            displayText = "";
        }
        else if (heldItem.GetType() == neededItem[0].GetType())
        {
            displayText = "Add Wood";

        }

        base.AvailableInteraction(heldItem, interactText);
    }


    public override void InteractedEvent()
    {
        base.InteractedEvent();
        if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType() == requiredItem.GetType())
        {
            StopAllCoroutines();
            angry = false;
            fx.Stop();
            PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].DestroyItem();
            PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] = null;
            TableManager.inst.ToggleTables(3);
            StartCoroutine(LifeCycle());
        }
    }

    IEnumerator LifeCycle()
    {
        float timeCoeff = (60f / GameTimer.inst.timeToMinute);
        float chargeTime = (15000 + angerDelay) / timeCoeff;
        while (chargeTime > 0)
        {
            pb.SetProgress(chargeTime / ((15000 + angerDelay) / timeCoeff));
            chargeTime -= 1 * Time.deltaTime;
            yield return null;
        }
        angry = true;
        yield return Mad();
        yield return new WaitForSeconds(750f / (60f / GameTimer.inst.timeToMinute));
        TableManager.inst.ToggleTables(3);
        angry = false;
        StartCoroutine(LifeCycle());
    }

    IEnumerator Mad()
    {
        float timeCoeff = (60f / GameTimer.inst.timeToMinute);
        float angryTime = 9000/timeCoeff;
        fx.Play();
        TableManager.inst.ToggleTables(2);
        while(angryTime > 0)
        {
            pb.SetProgress(angryTime / (9000 / timeCoeff));
            angryTime -= 1 * Time.deltaTime;
            yield return null;
        }
        fx.Stop();
        

        yield return null;
    }

    IEnumerator Shake()
    {
        yield return new WaitForSeconds(2.5f);
        CamShake.inst.ShakeCam();
    }

    public void Upgrade(string s, int i)
    {
        if(s == "Fire Place")
        {
            angerDelay += i;
        }
    }
}
