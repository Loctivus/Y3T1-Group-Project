using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : MonoBehaviour
{
    public Transform handPoint;
    public MeshRenderer[] allRenderers;
    public Texture2D[] textures;
    public float ditherValue;
    public AudioClip[] gossip;
    public AudioSource source;
    public void ActivateThisRace()
    {
        for (int i = 0; i < textures.Length; i++)
        {
            allRenderers[i].material.SetTexture("_Texture2D", textures[i]);
        }
        gameObject.SetActive(true);
        if (gossip.Length > 0)
        {
            source.clip = gossip[Random.Range(0, gossip.Length)];
            source.Play();
        }
    }
    private void Update()
    {
        UpdateAllRenderers();
    }
    void CastShadows(bool castShadows)
    {
        foreach (MeshRenderer r in allRenderers)
        {
            if(castShadows)
                r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            else
                r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }
    void UpdateAllRenderers()
    {
        foreach (MeshRenderer r in allRenderers)
        {
            r.material.SetFloat("_DitherValue", ditherValue);
        }
    }
    public IEnumerator DitherLerp(bool lerpIn)
    {
        if (lerpIn)
        {
            CastShadows(false);
            while (ditherValue <= 10.5f)
            {
                ditherValue += (0.05f*(60f / GameTimer.inst.timeToMinute)) * Time.deltaTime;
                UpdateAllRenderers();
                yield return null;
            }
            CastShadows(true);
        }
        else
        {
            CastShadows(false);
            while (ditherValue >= 0)
            {
                ditherValue -= (0.05f * (60f / GameTimer.inst.timeToMinute)) * Time.deltaTime;
                UpdateAllRenderers();
                yield return null;
            }
        }
        yield return null;
    }
}
