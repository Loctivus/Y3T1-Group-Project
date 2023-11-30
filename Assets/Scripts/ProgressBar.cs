using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image bar;
    public float progress;
    public float dangerZone;
    public Color dangerColor;
    public Color normalColor;

    public Image icon1;


    private void Update()
    {
        
    }

    public void SetProgress(float f)
    {
        if (bar == null)
        {
            return;
        }
        progress = f;
        bar.fillAmount = progress;
        if(progress < dangerZone)
        {
            bar.color = dangerColor;
        }
        else
        {
            bar.color = normalColor;
        }
    }
    public void SetIcon(Sprite s)
    {
        if (s != null)
        {
            icon1.enabled = true;
            icon1.sprite = s;
        }
        else
        {
            icon1.enabled = false;
        }
        
    }
}
