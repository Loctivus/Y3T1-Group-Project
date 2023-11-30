using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateEvents : MonoBehaviour
{
    // Declare singleton
    public static GameStateEvents inst;
    //Assigning to singleton
    private void Awake()
    {
        inst = this;
    }

    #region Game Init Event
    // Set up an event to manage on Start functionaility
    public event Action OnGameInit;
    public void GameInit()
    {
        //Use instead of ?invoke to catch null execption
        if (OnGameInit != null)
        {
            OnGameInit();
        }
    }
    #endregion

    #region Game Start Event
    // Set up an event to manage on Start functionaility
    public event Action OnGameStart;
    public void GameStart()
    {
        //Use instead of ?invoke to catch null execption
        if (OnGameStart != null)
        {
            OnGameStart();
        }
    }
    #endregion

    #region Game End Event
    // Set up an event to manage on End functionaility
    public event Action OnGameEnd;
    public void GameEnd()
    {
        //Use instead of ?invoke to catch null execption
        if (OnGameEnd != null)
        {
            OnGameEnd();
        }
    }
    #endregion

    #region Player Died Event
    // Set up an event to manage on Outro End functionality
    public event Action OnPlayerDied;
    public void PlayerDied()
    {
        //Use instead of ?invoke to catch null execption
        if (OnPlayerDied != null)
        {
            OnPlayerDied();
        }
    }
    #endregion

    #region Pause Event
    // Set up an event to manage on Outro End functionality
    public event Action OnPause;
    public void Pause()
    {
        //Use instead of ?invoke to catch null execption
        if (OnPause != null)
        {
            OnPause();
        }
    }
    #endregion

    #region UnPause Event
    // Set up an event to manage on Outro End functionality
    public event Action OnUnPause;
    public void UnPause()
    {
        //Use instead of ?invoke to catch null execption
        if (OnUnPause != null)
        {
            OnUnPause();
        }
    }
    #endregion

    #region OnBuyQuest Event
    // Set up an event to manage on Outro End functionality
    public event Action OnBuyQuest;
    public void BuyQuest()
    {
        //Use instead of ?invoke to catch null execption
        if (OnBuyQuest != null)
        {
            OnBuyQuest();
        }
    }
    #endregion

    #region OnQuestSelected Event
    // Set up an event to manage on Outro End functionality
    public event Action OnQuestSelected;
    public void QuestSelected()
    {
        //Use instead of ?invoke to catch null execption
        if (OnQuestSelected != null)
        {
            OnQuestSelected();
        }
    }
    #endregion

    #region StartDay Event
    // Set up an event to manage on Outro End functionality
    public event Action OnStartDay;
    public void StartDay()
    {
        //Use instead of ?invoke to catch null execption
        if (OnStartDay != null)
        {
            OnStartDay();
        }
    }
    #endregion

    #region EndDay Event
    // Set up an event to manage on Outro End functionality
    public event Action OnEndDay;
    public void EndDay()
    {
        //Use instead of ?invoke to catch null execption
        if (OnEndDay != null)
        {
            OnEndDay();
        }
    }
    #endregion

}
