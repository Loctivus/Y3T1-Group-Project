using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestController : Interactable
{
    public static QuestController inst;
    public QuestButton selectedQuest;
    [SerializeField] List<QuestButton> activeQuest = new List<QuestButton>();
    [SerializeField] List<TavernFolkData> questers = new List<TavernFolkData>();
    [SerializeField] List<int> activeQuestTime = new List<int>();

    public GameObject zoomedQuest;
    public Image stamp;
    public Image singleCost;
    public Image singleReward;
    public Image doubleCost;
    public Image doubleReward;
    public TMP_Text questTitle;
    public TMP_Text questCost1;
    public TMP_Text questCost2;
    public TMP_Text questDescription;
    public TMP_Text questReward1;
    public TMP_Text questReward2;
    public ScrollItem questScroll;
    
    

    //public GameObject questPanel;

    public bool inBoardRange;
    bool questBoardUp;
    public List<Image> questImages = new List<Image>();
    public List<QuestButton> questButtons = new List<QuestButton>();

    [HideInInspector] public List<GameObject> scrolls = new List<GameObject>();

    private void Awake()
    {
        inst = this;
        inBoardRange = false;
    }
    public void Start()
    {
        //GameStateEvents.inst.OnBuyQuest += PurchasedQuest;
        GameStateEvents.inst.OnStartDay += CheckCompletedQuest;
        GameStateEvents.inst.OnQuestSelected += FocusQuest;
        GameStateEvents.inst.OnGameStart += SetUp;
        GameStateEvents.inst.OnPlayerDied += ClearScrolls;
        Display(false);

    }



    public void AddQuester(TavernFolkData data)
    {
        questers.Add(data);
    }
    void Display(bool b)
    {
        
        foreach (var item in questImages)
        {
            item.enabled = b;
        }
        foreach (var item in questButtons)
        {
            item.Display(b);
        }
    }
    void ClearScrolls()
    {
        foreach (var item in scrolls)
        {
            if (item != null)
                Destroy(item.gameObject);
        }
        scrolls.Clear();
    }
    void SetUp()
    {
        Display(false);
        questBoardUp = false;
        selectedQuest = null;
        activeQuest.Clear();
        activeQuestTime.Clear();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inBoardRange && !questBoardUp)
            {
                questBoardUp = true;
                Cursor.lockState = CursorLockMode.Confined;
                Display(true);
            }
            else if ((inBoardRange && questBoardUp) || (!inBoardRange && questBoardUp))
            {
                questBoardUp = false;
                Cursor.lockState = CursorLockMode.Locked;
                Display(false);
            }
            
        }
    }
    public override void AvailableInteraction(Item heldItem, TMP_Text interactText)
    {
        displayText = "Open Quest Board";
        base.AvailableInteraction(heldItem, interactText);
    }

    public void GameStart()
    {
        activeQuest.Clear();
    }
    public void PurchasedQuest()
    {
        if (selectedQuest != null)
        {
            if (ResourceManager.inst.ValidPurchase(selectedQuest.primaryCost[selectedQuest.index],selectedQuest.primaryCostObj[selectedQuest.index].type))
            {
                if (selectedQuest.secondaryRewardObj[selectedQuest.index] != null)
                {

                    if (ResourceManager.inst.ValidPurchase(selectedQuest.secondaryCost[selectedQuest.index], selectedQuest.secondaryCostObj[selectedQuest.index].type))
                    {
                        //GameStateEvents.inst.BuyQuest();
                        ResourceManager.inst.Purchase(selectedQuest.secondaryCost[selectedQuest.index], selectedQuest.secondaryCostObj[selectedQuest.index].type);

                    }
                    else
                    {
                        return;
                    }
                }
                ResourceManager.inst.Purchase(selectedQuest.primaryCost[selectedQuest.index], selectedQuest.primaryCostObj[selectedQuest.index].type);
                PlayerInventory.pi.AddScroll(selectedQuest);

                //activeQuest.Add(selectedQuest);
                //activeQuestTime.Add(selectedQuest.daysToComplete);
                selectedQuest.Lock();
                UnfocusQuest();
                selectedQuest = null;
                Cursor.lockState = CursorLockMode.Locked;
                Display(false);
            }
        }
            
    }

    public void OrderQuest(QuestButton questScroll)
    {
        activeQuest.Add(questScroll);
        activeQuestTime.Add(questScroll.daysToComplete[questScroll.index]);
    }


    void CompleteQuest(QuestButton q)
    {
        ResourceManager.inst.RewardResources(q);
        
    }
    void CheckCompletedQuest()
    {
        List<QuestButton> deleteQuest = new List<QuestButton>();
        if(activeQuestTime.Count > 0)
        {
            for (int i = 0; i < activeQuestTime.Count; i++)
            {
                activeQuestTime[i] -= 1;
                if(activeQuestTime[i] <= 0)
                {
                    CompleteQuest(activeQuest[i]);
                    TableManager.inst.AddAdventurer(questers[i]);
                    questers.Remove(questers[i]);
                    deleteQuest.Add(activeQuest[i]);
                    activeQuestTime.Remove(activeQuestTime[i]);
                }
            }

        }
        for (int i = 0; i < deleteQuest.Count; i++)
        {
            if (activeQuest.Contains(deleteQuest[i]))
            {
                activeQuest.Remove(deleteQuest[i]);
            }
            if (activeQuestTime.Contains(0))
            {
                activeQuestTime.Remove(0);
            }
        }
        deleteQuest.Clear();
    }


    void UpdateQuestUI()
    {
        if (selectedQuest != null)
        {
            questCost2.text = " ";
            questReward2.text = " ";

            stamp.sprite = selectedQuest.stamp;
            questDescription.text = selectedQuest.descriptions[selectedQuest.index];
            singleCost.sprite = selectedQuest.primaryCostObj[selectedQuest.index].spriteSingle;
            questCost1.text = selectedQuest.primaryCost[selectedQuest.index].ToString();

            if (selectedQuest.secondaryCostObj[selectedQuest.index] != null)
            {
                doubleCost.enabled = true;
                doubleCost.sprite = selectedQuest.secondaryCostObj[selectedQuest.index].spriteDouble;
                questCost2.text = selectedQuest.secondaryCost[selectedQuest.index].ToString();
            }
            else
            {
                doubleCost.enabled = false;
            }

            singleReward.sprite = selectedQuest.primaryRewardObj[selectedQuest.index].spriteSingle;
            questReward1.text = selectedQuest.primaryReward[selectedQuest.index].ToString();
            if (selectedQuest.secondaryRewardObj[selectedQuest.index] != null)
            {
                doubleReward.enabled = true;
                doubleReward.sprite = selectedQuest.secondaryRewardObj[selectedQuest.index].spriteDouble;
                questReward2.text = selectedQuest.secondaryReward[selectedQuest.index].ToString();
            }
            else
            {
                doubleReward.enabled = false;
            }



            //questTitle.text = selectedQuest.title;
            //questCost.text = "Cost To Purchase Quest " + selectedQuest.costs[selectedQuest.index].ToString();
            //questDescription.text = selectedQuest.description;
            //questRewards.text = selectedQuest.rewards;
        }
    }
    void FocusQuest()
    {
        UpdateQuestUI();
        zoomedQuest.SetActive(true);
    }

    public void UnfocusQuest()
    {
        zoomedQuest.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            inBoardRange = true;
            
            //questPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.Locked;
            inBoardRange = false;
            Display(false);
        }
    }
}
