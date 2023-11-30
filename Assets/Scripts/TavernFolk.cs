using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernFolk : MonoBehaviour
{
    public CharacterVisuals charV;
    public TavernFolkData myData;
    public Race[] allRaces;
    public Race myRace;
    public Table myTable;
    public Seat mySeat;
    public Transform hand;
    [HideInInspector] public bool hasDrink;
    [HideInInspector] public float totalWaitTime;
    [HideInInspector] public bool satsfied;
    public Sprite wantDrink;
    public Sprite drinking;

    public ProgressBar pb;

    public States currentState;
    public enum States
    {
        Init,
        Arriving,
        Waiting,
        WaitingAngry,
        Drinking,
        Leaving

    }

    
    public virtual void Start()
    {
        myRace = allRaces[Random.Range(0, allRaces.Length)];

        myRace.ActivateThisRace();
        hand = myRace.handPoint;
        GameStateEvents.inst.OnEndDay += LeaveNow;
        totalWaitTime = myData.waitTime;
        StartCoroutine(Arriving());
    }
    public void GivenDrink()
    {
        hasDrink = true;
        mySeat.PickUpCup(hand);
        StopAllCoroutines();
        StartCoroutine(Drinking());
    }
    void LeaveEarly()
    {
        

    }
    void LeaveNow()
    {
        StartCoroutine(Leaving(false));
    }
    
    public void PayGold()
    {
        ResourceManager.inst.MadeSale(1);
    }

    IEnumerator Arriving()
    {
        pb.SetProgress(0f);
        pb.SetIcon(null);
        currentState = States.Arriving;
        yield return myRace.DitherLerp(true);
        StartCoroutine(Waiting());
    }
    public virtual IEnumerator Leaving(bool satisfied)
    {
        
        if (hasDrink)
            mySeat.PutDownCup();

        yield return myRace.DitherLerp(false);
        GameStateEvents.inst.OnEndDay -= LeaveNow;

        myTable.ClearSpot(this, mySeat);
        if (gameObject != null)
            Destroy(gameObject);
    }
    public virtual IEnumerator Waiting()
    {
        pb.dangerZone = 0;
        yield return null;

    }
    public IEnumerator UnhappyWaiting()
    {
        charV.Angry(true);
        currentState = States.WaitingAngry;
        pb.dangerZone = 1;
        float timeCoeff = (60f / GameTimer.inst.timeToMinute);
        float angryTime = (totalWaitTime) / timeCoeff;
        pb.SetIcon(wantDrink);
        while (angryTime > 0)
        {
            pb.SetProgress(angryTime / (totalWaitTime / timeCoeff));
            angryTime -= 1 * Time.deltaTime;
            yield return null;
        }
        pb.SetIcon(null);
        StartCoroutine(Leaving(false));
    }

    IEnumerator Drinking()
    {
        charV.Angry(false);
        currentState = States.Drinking;
        float timeCoeff = (60f / GameTimer.inst.timeToMinute);
        float angryTime = (totalWaitTime / timeCoeff);
        pb.dangerZone = 0;
        pb.SetIcon(drinking);
        while (angryTime > 0)
        {
            pb.SetProgress(angryTime / (totalWaitTime/ timeCoeff));
            angryTime -= 1 * Time.deltaTime;
            yield return null;
        }
        pb.SetIcon(null);
        StartCoroutine(Leaving(true));
    }
    
}
