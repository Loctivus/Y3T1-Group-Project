using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public static CamShake inst;

    public Animator anim;
    private void Start()
    {
        inst = this;
    }
    public void ShakeCam()
    {
        anim.SetTrigger("Shake");
    }
}
