using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public class TavernFolkData : ScriptableObject
{
    public new string name;
    public string description;
    public int waitTime;

}
