using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager inst;
    public GameObject customerEmpty;
    public GameObject adventurerEmpty;
    public List<GameObject> standardFurniture = new List<GameObject>();
    public List<GameObject> skeletonFurniture = new List<GameObject>();

    public float peakPointGain = 10;
    public float pointGain = 5;
    public float points;
    float maxFloat = 100;
    [SerializeField] List<TavernFolkData> everyAdventurers = new List<TavernFolkData>();
    [SerializeField] List<TavernFolkData> availableAdventurers = new List<TavernFolkData>();
    [SerializeField] List<TavernFolkData> availableCustomers = new List<TavernFolkData>();
    [SerializeField] List<Table> allTables = new List<Table>();
    void Awake()
    {
        inst = this;
    }
    public void AddAdventurer(TavernFolkData data)
    {
        if (!availableAdventurers.Contains(data))
            availableAdventurers.Add(data);
    }
    private void Start()
    {
        GameStateEvents.inst.OnStartDay += StartDay;
        GameStateEvents.inst.OnGameStart += SetUp;
    }
    private void StartDay()
    {
        availableAdventurers = new List<TavernFolkData>(everyAdventurers);
        points = 0;

        foreach (Table table in allTables)
        {
            table.mrS = GetComponentsInChildren<MeshRenderer>();
        }
    }
    void FixedUpdate()
    {
        if (StateManager.inst.gamePlaying)
        {
            if (GameTimer.inst.curState != GameTimer.GameStates.Night && GameTimer.inst.curState != GameTimer.GameStates.Prep)
            {
                if (points < maxFloat)
                {
                    if (GameTimer.inst.curState == GameTimer.GameStates.Peak)
                    {
                        points += peakPointGain * Time.deltaTime;
                    }
                    else
                    {
                        points += pointGain * Time.deltaTime;
                    }
                }
            }

            if (CanAfford(false, 50f))
            {
                Debug.Log("Spawn");
                DecideOnSpawn();
            }
        }
    }
    void SetUp()
    {
        peakPointGain = 10;
        pointGain = 5;
        points = 0;
        maxFloat = 100;

    }
    public void ToggleTables(int i)
    {
        foreach (Table table in allTables)
        {

            table.DirtyTable(i);
        }
    }

    Table FreeTable()
    {
        foreach (Table t in allTables)
        {
            if(t.currentState.Count == 0)
            {
                if (t.GetFreeSeat(false) != null)
                {
                    return t;
                }
            }
           
        }
        return null;
    }

    void DecideOnSpawn()
    {
        Table t = FreeTable();
        if(t != null)
        {
            CanAfford(true, 50f);
            if (Random.Range(0, 100) > 50)
            {
                if(availableAdventurers.Count >= 1)
                {
                    t.Spawn(adventurerEmpty, availableAdventurers[0]);
                    availableAdventurers.Remove(availableAdventurers[0]);
                }
                else
                {
                    t.Spawn(customerEmpty, availableCustomers[0]);
                    availableCustomers.Add(availableCustomers[0]);
                    availableCustomers.RemoveAt(0);
                }
            }
            else
            {
                t.Spawn(customerEmpty, availableCustomers[0]);
                availableCustomers.Add(availableCustomers[0]);
                availableCustomers.RemoveAt(0);
            }
            
        }
        
    }

    public bool CanAfford(bool change, float amount)
    {
        if(points - amount >= 0)
        {
            if (change)
            {
                points -= amount;
            }
            return true;
        }
        return false;
    }

    public void Upgrade(string s)
    {
        if(s == "Spawn rate")
        {
            pointGain *= 1.25f;
            peakPointGain *= 1.25f;
        }
    }

}
