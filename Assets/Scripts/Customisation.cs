using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customisation : MonoBehaviour
{
    public GameObject[] chairMeshes;
    public GameObject[] stoolMeshes;
    public GameObject[] tableSquareMeshes;
    public GameObject[] tableRoundMeshes;
    public GameObject[] firePlace;
    public GameObject[] bar;
    public GameObject[] foodCounter;

    public GameObject[] chairMeshesS;
    public GameObject[] stoolMeshesS;
    public GameObject[] tableSquareMeshesS;
    public GameObject[] tableRoundMeshesS;
    public GameObject[] firePlaceS;
    public GameObject[] barS;
    public GameObject[] foodCounterS;


    bool chairBought;
    bool stoolBought;
    bool tableRBought;
    bool tableSBought;
    bool fireBought;
    bool barBought;
    bool foodBought;

    public CurrencyUpdater goldRef;

    public void SelectChair(int i)
    {
        if (!chairBought)
        {
            if (PurchaseItem(chairBought))
            {
                SetItem(chairMeshes, chairMeshesS, true);
            }
        }
        else
        {
            SetItem(chairMeshes, chairMeshesS, true);
        }
    }

    public void SelectStool(int i)
    {
        if (!stoolBought)
        {
            if (PurchaseItem(stoolBought))
            {
                SetItem(stoolMeshes, stoolMeshesS, true);
            }
        }
        else
        {
            SetItem(stoolMeshes, stoolMeshesS, true);
        }
    }

    public void SelectTableRound(int i)
    {
        if (!tableRBought)
        {
            if (PurchaseItem(tableRBought))
            {
                SetItem(tableRoundMeshes, tableRoundMeshesS, true);
            }
        }
        else
        {
            SetItem(tableRoundMeshes, tableRoundMeshesS, true);
        }
    }

    public void SelectTableSquare(int i)
    {
        if (!tableSBought)
        {
            if (PurchaseItem(tableSBought))
            {
                SetItem(tableSquareMeshes, tableSquareMeshesS, true);
            }
        }
        else
        {
            SetItem(tableSquareMeshes, tableSquareMeshesS, true);
        }
    }
    public void SelectFire(int i)
    {
        if (!fireBought)
        {
            if (PurchaseItem(fireBought))
            {
                SetItem(firePlace, firePlaceS, true);
            }
        }
        else
        {
            SetItem(firePlace, firePlaceS, true);
        }
    }

    public void SelectBar(int i)
    {
        if (!barBought)
        {
            if (PurchaseItem(barBought))
            {
                SetItem(bar, barS, true);
            }
        }
        else
        {
            SetItem(bar, barS, true);
        }
    }

    public void SelectFood(int i)
    {
        if (!foodBought)
        {
            if (PurchaseItem(foodBought))
            {
                SetItem(foodCounter, foodCounterS, true);
            }
        }
        else
        {
            SetItem(foodCounter, foodCounterS, true);
        }
    }





    bool PurchaseItem(bool b)
    {
        if(goldRef.currentBalance - 100 >= 0)
        {
            //ResourceManager.inst.Purchase(100, "Gold");
            goldRef.ReduceBalance(100);
            b = true;
            return true;
        }
        return false;
    }

    void SetItem(GameObject[] original , GameObject[] next , bool b)
    {
        Debug.Log("Set new furniture");
        for (int i = 0; i < original.Length; i++)
        {
            original[i].SetActive(!b);
            next[i].SetActive(b);
        }
    }



}
