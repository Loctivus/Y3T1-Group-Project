using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameTimer : MonoBehaviour
{ 
    public static GameTimer inst;
    public int day = 1;
    public int hours = 0;
    public int minutes = 0;
    public float time = 0;
    public float timeToMinute = 60;
    public float offset;

    public GameObject clockFace;
    public TMP_Text timeText;
    public TMP_Text dayText;
    public TMP_Text stateText;

    public GameObject endOfDayReport;

    public enum GameStates
    {
        Init,
        Start,
        Prep,
        Open,
        Peak,
        Close,
        Closing,
        Night,

    }

    public GameStates curState;

    void Awake()
    {
        inst = this;
        //endOfDayReport.SetActive(false);
    }

    private void Start()
    {
        GameStateEvents.inst.OnGameInit += SetUp;
        GameStateEvents.inst.OnGameStart += GameStart;
        GameStateEvents.inst.OnStartDay += StartDay;
    }
    void SetUp()
    {
        curState = GameStates.Init;
    }

    void GameStart()
    {
        curState = GameStates.Start;
        day = 1;
        hours = 10;
        minutes = 0;
        time = 0;
}

    // Update is called once per frame
    void Update()
    {
        if (StateManager.inst.gamePlaying)
        {
            if (curState != GameStates.Night)
                time += Time.deltaTime;

            if (time >= timeToMinute)
            {
                time = 0;
                minutes++;
            }

            if (minutes >= 59)
            {
                minutes = 0;
                hours++;

            }
            if (hours >= 24)
            {
                hours = 0;
                day += 1;
            }

            timeText.text = hours.ToString();
            dayText.text = "Day " + day.ToString();

            if (hours >= 10 && hours < 12)
            {
                curState = GameStates.Prep;
                stateText.text = "Prep Time";
            }
            else if (hours >= 12 && hours < 18)
            {
                curState = GameStates.Open;
                stateText.text = "Open Time";
            }
            else if (hours >= 18 && hours < 22)
            {
                curState = GameStates.Peak;
                stateText.text = "Peak Time";
            }
            else if (hours >= 22 && hours < 24)
            {
                curState = GameStates.Open;
                stateText.text = "Open Time";
            }
            else if (hours >= 0 && hours < 1)
            {
                curState = GameStates.Close;
                stateText.text = "Close Time";
            }
            else if (hours >= 1 && hours < 2)
            {
                curState = GameStates.Closing;
                stateText.text = "Prep Time";
            }
            else if (hours >= 2 && hours < 10)
            {
                curState = GameStates.Night;
                EndDay();
                stateText.text = "Bed Time";
            }

            clockFace.transform.localRotation = Quaternion.Euler(0, 0, offset + (((hours * 60) + minutes) * 0.25f));
        }
        
    }

    void EndDay()
    {
        GameStateEvents.inst.EndDay();
    }

    void StartDay()
    {
        hours = 10;
        minutes = 0;
        //endOfDayReport.SetActive(false);
        curState = GameStates.Start;
        //QuestManager.qMInst.CheckQuests(day);
    }
}
