using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spill : MonoBehaviour
{
    int health;
    public int maxHealth;
    public bool active;
    public GameObject spillObj;
    public MeshRenderer mr;
    private void Start()
    {
        GameStateEvents.inst.OnGameStart += Setup;
    }

    void Setup()
    {
        float r = Random.Range(0,100);
        if(r> 75)
        {
            AddSpill();
        }
        else
        {
            RemoveSpill();
        }
    }

    public void AddSpill()
    {
        active = true;
        health = maxHealth;
        spillObj.SetActive(true);
    }

    public void RemoveSpill()
    {
        active = false;
        health = 0;
        spillObj.SetActive(false);
    }

    public void CleanSpill()
    {
        health -= 35;
        mr.material.color = new Color(1,1,1, ((float)health / maxHealth));
        if(health <= 0)
        {
            RemoveSpill();
        }
    }
}
