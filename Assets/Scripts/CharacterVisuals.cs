using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CharacterVisuals : MonoBehaviour
{
    public VisualEffect angryIndicator;

    public void Angry(bool b)
    {
        if (b)
            angryIndicator.Play();
        else
            angryIndicator.Stop();
    }
}
