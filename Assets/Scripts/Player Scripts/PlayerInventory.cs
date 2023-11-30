using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory pi;

    public ScrollItem emptyScroll;
    public int currentHeldItem;
    public Transform playerHand;
    bool swappingItems;

    public List<Item> items = new List<Item>();
    public Animator anim;
    
    void Awake()
    {
        pi = this;
    }

    void Start()
    {
        GameStateEvents.inst.OnGameInit += ClearInventory;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && items[currentHeldItem] != null)
        {
            items[currentHeldItem].DropItem();
            PlayerInteract.pin.ObjInRangePrioritise();
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (swappingItems == false)
            {
                currentHeldItem++;
                swappingItems = true;

                if (currentHeldItem == items.Count)
                {
                    currentHeldItem = 0;

                }
                anim.SetTrigger("Change");
                StartCoroutine(SetItemVisibility());
            }
        }
    }

    void ClearInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i] = null;
        }
    }

    public IEnumerator SetItemVisibility()
    {
        yield return new WaitForSeconds(0.15f);
        foreach (var item in items)
        {
            if (item != null)
            {
                item.gameObject.transform.parent.localScale = Vector3.one;
                item.gameObject.SetActive(false);
            }
        }


        if (items[currentHeldItem] != null)
        {
            items[currentHeldItem].gameObject.SetActive(true);
        }

        swappingItems = false;
    }

    public bool ChangeItem(Item newItem)
    {
        int i = GetFreeSpot();

        if (items[currentHeldItem] == null)
        {
            items[currentHeldItem] = newItem;
            return true;
        }

        if (items[currentHeldItem] == newItem)
        {
            items[currentHeldItem] = null;
            return false;
        }

        return false;
    }

    public int GetFreeSpot()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                return i;
            }
        }

        return -1;
    }


    public void AddScroll(QuestButton newScroll)
    {
        ScrollItem purchasedScroll = Instantiate(emptyScroll);
        purchasedScroll.cl.enabled = false;
        purchasedScroll.myData = newScroll;
        QuestController.inst.scrolls.Add(purchasedScroll.gameObject);
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = purchasedScroll;
                purchasedScroll.transform.parent = playerHand;
                purchasedScroll.transform.position = playerHand.position;
                return;
            }          
        }

        purchasedScroll.transform.position = gameObject.transform.position;

    }
    public void Upgrade(string s, int n)
    {
        if (s == "Inventory Space")
        {
            for (int i = 0; i < n; i++)
            {
                items.Add(null);
            }

        }
    }
}
