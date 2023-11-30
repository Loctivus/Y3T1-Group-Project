using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public bool resetToLocked;
    public bool locked;
    public bool endQuest;

    //public QuestData myQuest;

    [HideInInspector] public int index = 0;
    
    public int[] daysToComplete;
    
    public QuestButton[] rewardQuest;
    
    public int dayCount;

    public string[] descriptions;

    public Sprite stamp;

    [SerializeField]Image stampI;
    [SerializeField] Image quest;
    [SerializeField] Button questB;

    [Space(15)]
    [Header("Primary Cost")]
    [Space(5)]
    public int[] primaryCost;
    public Cost[] primaryCostObj;

    [Header("Secondary Cost")]
    [Space(5)]
    public int[] secondaryCost;
    public Cost[] secondaryCostObj;

    [Space(15)]
    //public Sprite[] doubleCost;
    [Header("Primary Rewards")]
    [Space(5)]
    public int[] primaryReward;
    public Reward[] primaryRewardObj;

    [Header("Secondary Rewards")]
    [Space(5)]
    public int[] secondaryReward;
    public Reward[] secondaryRewardObj;
    private void Start()
    {
        GameStateEvents.inst.OnGameStart += SetUp;
        GameStateEvents.inst.OnStartDay += CanUnlock;       
    }
    public void Display(bool b)
    {
        bool a = b && !locked;
        if(quest != null)
            quest.enabled = a;
        if(questB != null)
            questB.enabled = a;
        if(stampI != null)
            stampI.enabled = a;
        
    }
    void SetUp()
    {
        if (resetToLocked || locked)
            Lock();
        if (!resetToLocked)
            Unlock();
        index = 0;
    }
    public void SelectQuest()
    {
        QuestController.inst.selectedQuest = this;
        GameStateEvents.inst.QuestSelected();
    }
    void CanUnlock()
    {
        Debug.Log("Unlocked Nodes Run Func");
        if (locked)
            Lock();
        if (dayCount != 0)
        {
            if(dayCount <= GameTimer.inst.day)
            {
                Debug.Log("Unlocked Nodes");
                Unlock();
            }
        }
        
    }
    public void Lock()
    {
        locked = true;
        //Display(false);
    }

    public void Unlock()
    {
        if (endQuest && index >= daysToComplete.Length)
            return;
        locked = false;
        //Display(true);
    }

    public void AddToIndex()
    {
        if(index < daysToComplete.Length-1)
        {
            index += 1;
        }
    }
}
