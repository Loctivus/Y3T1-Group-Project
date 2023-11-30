using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class QuestData : ScriptableObject 
{
    public new string name;
    public string description;
    public string rewards;
    public int cost;
    public Reward reward;
    public int daysToComplete;

}
