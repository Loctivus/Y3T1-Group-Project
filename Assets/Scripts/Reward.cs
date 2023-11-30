using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Reward", menuName = "Rewards/Reward")]

public class Reward : ScriptableObject
{
    public string type;
    public Sprite spriteSingle;
    public Sprite spriteDouble;
 
}
