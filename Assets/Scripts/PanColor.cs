using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PanColor : MonoBehaviour
{
    public VisualEffect fx;
    float usableTime;
    public Gradient colour;

    void Start()
    {
        fx.SetGradient("Colour", colour);
    }
    // Update is called once per frame
    void Update()
    {
        usableTime = (((GameTimer.inst.hours * 60) + GameTimer.inst.minutes) + GameTimer.inst.time) / 1440f;
        fx.SetFloat("PanValue", usableTime);
    }
}
