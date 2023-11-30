using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public MopItem mop;

    public void EndMop()
    {
        mop.EndSweep();
    }
}
