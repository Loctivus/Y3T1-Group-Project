using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager inst;
    public Sink sinkRef;
    public CurrencyUpdater goldRef;
    public CurrencyUpdater mushroomRef;
    public CurrencyUpdater graveRef;
    public CurrencyUpdater wandRef;

    public int goldPerDrink;

    //public TMP_Text moneyText;
    //public TMP_Text moneyChangedText;
    public AudioSource moneySFX;
    //int newCoinBalance;

    [HideInInspector]
    public int gold;
    [HideInInspector]
    public int glitter;
    [HideInInspector]
    public int tombDust;
    [HideInInspector]
    public int mushrooms;

    public int startGold;
    public int startMushrooms;
    public int startTombDust;
    public int startGlitter;

    private void Awake()
    {
        inst = this;
    }
    void Start()
    {
        GameStateEvents.inst.OnGameInit += SetUp;
        //GameStateEvents.inst.OnBuyQuest += Purchase;
    }

    void SetUp()
    {
        goldRef.currentBalance = startGold;
        wandRef.currentBalance = startGlitter;
        graveRef.currentBalance = startTombDust;
        mushroomRef.currentBalance = startMushrooms;
    }

    public bool ValidPurchase(int cost, string s)
    {
        if (s == "Gold")
        {
            if (goldRef.currentBalance >= cost)
                return true;
        }
        else if (s == "Wand")
        {
            if (wandRef.currentBalance >= cost)
                return true;
        }
        else if (s == "Shroom")
        {
            if (mushroomRef.currentBalance >= cost)
                return true;
        }
        else if (s == "Grave")
        {
            if (graveRef.currentBalance >= cost)
                return true;
        }
        else
        {
            if (goldRef.currentBalance > cost)
                return true;
        }

        return false;

    }

    public void Purchase(int cost, string s)
    {
        if (s == "Gold")
        {
            goldRef.ReduceBalance(cost);
        }
        else if (s == "Wand")
        {
            wandRef.ReduceBalance(cost);
        }
        else if (s == "Shroom")
        {
            mushroomRef.ReduceBalance(cost);
        }
        else if (s == "Grave")
        {
            graveRef.ReduceBalance(cost);
        }
        else
        {
            goldRef.ReduceBalance(cost);
        }
    }



    void RewardSwitch(string s, int i)
    {
        switch (s)
        {
            case "Gold":
                goldRef.IncreaseBalance(i);
                break;

            case "Wand":
                wandRef.IncreaseBalance(i);
                break;

            case "Shroom":
                mushroomRef.IncreaseBalance(i);
                break;

            case "Grave":
                graveRef.IncreaseBalance(i);
                break;

            case "Inventory Space":
                PlayerInventory.pi.Upgrade(s, i);
                break;

            case "Clean Sink Space":
                Sink.inst.UpgradeCleanedCapacity(s, i);
                break;
            case "Drink Value":
                goldPerDrink += i;
                break;
            case "GrogCrate":
                DeliveryShelfContainer.dscInst.AddCrate(s, i);
                break;
            case "WoodCrate":
                DeliveryShelfContainer.dscInst.AddCrate(s, i);
                break;
            case "Spawn Rate":
                TableManager.inst.Upgrade(s);
                break;
            case "Fire Place":
                //FirePlace.inst.Upgrade(s, i);
                break;
        }

    }

    public void RewardResources(QuestButton q)
    {        
        //currentGold += q.goldReward[q.index];
        //PlayerInventory.pi.UpgradeInventory(q.deliveryReward[q.index]);
        //sinkRef.UpgradeCleanedCapacity(q.deliveryReward[q.index]);

        moneySFX.Play();
        q.rewardQuest[q.index].Unlock();
        q.AddToIndex();
        q.Unlock();

        if (q.primaryReward[q.index] > 0)
        {
            string s = q.primaryRewardObj[q.index].type;
            RewardSwitch(s,q.primaryReward[q.index]);
        }
        if (q.secondaryReward[q.index] > 0)
        {
            string s = q.secondaryRewardObj[q.index].type;
            RewardSwitch(s,q.secondaryReward[q.index]);
        }


        //StartCoroutine(UpdateResourceUI());
    }

    public void MadeSale(int numberSold)
    {
        /*
        goldLossGain = numberSold * goldPerDrink;
        moneyChangedText.text = "+" + goldLossGain; 
        newCoinBalance = currentGold + goldLossGain;
        moneySFX.Play();
        StartCoroutine(UpdateResourceUI());
        //UpdateInGameUI();
        */

        goldRef.IncreaseBalance(numberSold * goldPerDrink);
        moneySFX.Play();
    }

    /*
    IEnumerator UpdateResourceUI()
    {
        if (currentGold != newCoinBalance)
        {
            if (newCoinBalance > currentGold)
            {
                currentGold++;
                moneyText.text = currentGold.ToString();
                yield return new WaitForSeconds(0.25f);
                StartCoroutine(UpdateResourceUI());
            }
            else if (newCoinBalance < currentGold)
            {
                currentGold--;
                moneyText.text = currentGold.ToString();
                yield return new WaitForSeconds(0.25f);
                StartCoroutine(UpdateResourceUI());
            }    
        }
        else
        {
            moneyChangedText.text = "";
        }
    }
    */
}
