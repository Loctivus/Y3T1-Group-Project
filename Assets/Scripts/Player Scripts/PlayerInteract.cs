using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract pin;
    public List<Interactable> objectsInRange = new List<Interactable>();
    public TMP_Text interactText;
    public GameObject interactKeyImg;
    public List<GameObject> cycleObjsUI = new List<GameObject>();
    int priorityObjNum = 0;

    [HideInInspector]
    public Interactable thisInteractable;

    private void Awake()
    {
        interactText.transform.gameObject.SetActive(false);
        interactKeyImg.SetActive(false);

        pin = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            thisInteractable = other.GetComponent<Interactable>();

            if (!objectsInRange.Contains(thisInteractable))
            {
                thisInteractable.AvailableInteraction(PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem], interactText);
                //Debug.Log("inside Trigger");
                for (int i = 0; i < thisInteractable.neededItem.Length; i++)
                {
                    Item thisItem = thisInteractable.neededItem[i];

                    if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] != null && thisItem != null)
                    {
                        //Debug.Log("passed item check");
                        if (thisItem.GetType() == PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType())
                        {
                            //Debug.Log("Passed type check");
                            objectsInRange.Add(thisInteractable);
                            CheckObjNum();
                            ObjInRangePrioritise();
                            break;
                        }
                    }
                    else if (thisItem == PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem])
                    {
                        objectsInRange.Add(thisInteractable);
                        CheckObjNum();
                        ObjInRangePrioritise();
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            thisInteractable = other.GetComponent<Interactable>();

            if (!objectsInRange.Contains(thisInteractable))
            {
                thisInteractable.AvailableInteraction(PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem], interactText);
                Debug.Log("inside Trigger");
                for (int i = 0; i < thisInteractable.neededItem.Length; i++)
                {
                    Item thisItem = thisInteractable.neededItem[i];

                    if (PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem] != null && thisItem != null)
                    {
                        Debug.Log("passed item check");
                        if (thisItem.GetType() == PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem].GetType())
                        {
                            Debug.Log("Passed type check");
                            objectsInRange.Add(thisInteractable);
                            CheckObjNum();
                            ObjInRangePrioritise();
                            break;
                        }
                    }
                    else if (thisItem == PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem])
                    {
                        objectsInRange.Add(thisInteractable);
                        CheckObjNum();
                        ObjInRangePrioritise();
                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            Interactable thisInteractable = other.GetComponent<Interactable>();
            if (objectsInRange.Contains(thisInteractable))
            {
                objectsInRange.Remove(thisInteractable);
                CheckObjNum();
                ObjInRangePrioritise();

                if (objectsInRange.Count == 0)
                {
                    HideUIPrompt();
                }
            }
        }
    }

    void CheckObjNum()
    {
        if (objectsInRange.Count > 1)
        {
            for (int i = 0; i < cycleObjsUI.Count; i++)
            {
                cycleObjsUI[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < cycleObjsUI.Count; i++)
            {
                cycleObjsUI[i].SetActive(false);
            }
        }
    }

    public void ObjInRangePrioritise()
    {
        if (objectsInRange.Count > 1)
        {
            priorityObjNum++;
            if (priorityObjNum >= objectsInRange.Count)
            {
                priorityObjNum = 0;
            }

            objectsInRange[priorityObjNum].AvailableInteraction(PlayerInventory.pi.items[PlayerInventory.pi.currentHeldItem], interactText);
        }
        else
        {
            priorityObjNum = 0;
        }
    }

    void Update()
    {
        if (objectsInRange.Count >= 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pick up please");
                objectsInRange[priorityObjNum].InteractedEvent();
                CheckObjNum();
            }
            else if(Input.GetKeyDown(KeyCode.F))
            {
                ObjInRangePrioritise();
            }
        }
        else
        {
            HideUIPrompt();
        }
        
        
    }

    public void ShowUIPrompt( string interactString) 
    {
        interactText.transform.gameObject.SetActive(true);
        interactText.text = interactString;
        interactKeyImg.SetActive(true);
    }
    public void HideUIPrompt()
    {
        interactKeyImg.SetActive(false);
        interactText.transform.gameObject.SetActive(false);
    }
}
