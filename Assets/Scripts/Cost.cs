using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Cost", menuName = "Costs/Cost")]

public class Cost : ScriptableObject
{
    public string type;
    public Sprite spriteSingle;
    public Sprite spriteDouble;

}