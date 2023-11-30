using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PostProcessingDayNight : MonoBehaviour
{
    public Volume vol;
    public ChannelMixer volChannel;
    public AnimationCurve redChannelRedCurve;

    public float usableTime;
    private void Awake()
    {
        if (vol.profile.TryGet<ChannelMixer>(out ChannelMixer tmp))
        {
            volChannel = tmp;
        }
    }
    void Update()
    {
        usableTime = (((GameTimer.inst.hours * 60) + GameTimer.inst.minutes)+ GameTimer.inst.time)/1440f;
        ClampedFloatParameter clampedTime = new ClampedFloatParameter(redChannelRedCurve.Evaluate(usableTime), volChannel.redOutRedIn.min, volChannel.redOutRedIn.max);
        volChannel.redOutRedIn.SetValue(clampedTime);
    }
}
