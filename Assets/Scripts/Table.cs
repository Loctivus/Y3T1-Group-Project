using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public MeshRenderer mr;
    public MeshRenderer[] mrS;
    public bool Valid;
    public Seat[] allSeats;
    List<TavernFolk> customersAtTable = new List<TavernFolk>();
    public List<tableStates> currentState = new List<tableStates>();
    public Spill[] allSpills;
    float[] spillValues = new float[3];

    public enum tableStates
    {
        Full,
        Burnt,
        Dirty
    }

    void AddTableState(tableStates t)
    {
        if(!currentState.Contains(t))
            currentState.Add(t);
    }

    void RemoveTableStates(tableStates t)
    {
        if (currentState.Contains(t))
            currentState.Remove(t);
    }

    public void DirtyTable(int i)
    {
        if(i == 1) {
            
            AddTableState(tableStates.Dirty);
            //mr.material.SetFloat("_Float", Random.Range(-5, 5));
            //mr.material.SetColor("_Dirt_Color", new Color(0.2f, 0.8f, 0.3f)*3);
        }
        else if (i == 2)
        {
            mr.material.SetInt("_Boolean", 1);
            AddTableState(tableStates.Burnt);
            //mr.material.SetFloat("_Float", Random.Range(-5, 5));
            //mr.material.SetColor("_Dirt_Color", new Color(0.8f, 0.3f, 0.2f) *5);
        }
        else if (i == 0)
        {
            RemoveTableStates(tableStates.Dirty);
        }
        else if (i == 3)
        {
            mr.material.SetInt("_Boolean", 0);
            RemoveTableStates(tableStates.Burnt);
        }
        
    }

    public void Spawn(GameObject t, TavernFolkData s)
    {
        
        Seat freeSeat = GetFreeSeat(true);
        if(freeSeat != null)
        {
            var newPerson = Instantiate(t);
            TavernFolk tf = newPerson.GetComponent<TavernFolk>();
            tf.myData = s;
            tf.myTable = this;
            tf.mySeat = freeSeat;
            freeSeat.folkInSeat = tf;
            newPerson.transform.position = freeSeat.seatPos.position;
            newPerson.transform.rotation = freeSeat.seatPos.rotation;
            newPerson.transform.localScale = freeSeat.seatPos.localScale;
            customersAtTable.Add(tf);
            //AddSpill();
            
        }
    }
    public void ClearSpot(TavernFolk tf, Seat s)
    {
        if (customersAtTable.Contains(tf))
            customersAtTable.Remove(tf);
        if (customersAtTable.Contains(null))
            customersAtTable.Remove(null);
        RemoveTableStates(tableStates.Full);
        AddSpill();
        s.inUse = false;

    }

    public Seat GetFreeSeat(bool changeSeat)
    {
        foreach (Seat s in allSeats)
        {
            if (!s.inUse)
            {
                if (changeSeat)
                {
                    s.inUse = true;
                }
                return s;
            }
        }
        AddTableState(tableStates.Full);
        return null;
    }

    void AddSpill()
    {
        for (int i = 0; i < allSpills.Length; i++)
        {
            if (!allSpills[i].active)
            {
                allSpills[i].AddSpill();
                
                return;
            }
            
        }
        int count = 0;
        for (int i = 0; i < allSpills.Length; i++)
        {
            if (allSpills[i].active)
            {
                count += 1;
            }

        }
        if (count >= 2)
            DirtyTable(1);
    }

    public void CleanSpill()
    {
        for (int i = 0; i < allSpills.Length; i++)
        {
            if (allSpills[i].active)
            {
                allSpills[i].CleanSpill();
                return;
            }

        }
        int count = 0;
        for (int i = 0; i < allSpills.Length; i++)
        {
            if (allSpills[i].active)
            {
                count += 1;
            }

        }
        if (count < 2)
            DirtyTable(0);
    }

}
