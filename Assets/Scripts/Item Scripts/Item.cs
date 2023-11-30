using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public Collider cl;
    public float groundOffset;
    public override void InteractedEvent()
    {
        base.InteractedEvent();
        
        if (PlayerInventory.pi.ChangeItem(this))
        {
            AddItem();
        }
        
    }


    public void AddItem()
    {
        transform.parent = PlayerInventory.pi.playerHand;
        transform.position = PlayerInventory.pi.playerHand.position;
        cl.enabled = false;
        PlayerInteract.pin.objectsInRange.Remove(this);
    }   
    
    public void DropItem()
    {
        transform.parent = null;
        PlayerInventory.pi.ChangeItem(this);

        cl.enabled = true;
        PlayerInteract.pin.objectsInRange.Add(this); // careful if bugs caused by this
        AvailableInteraction(this, PlayerInteract.pin.interactText);
        PlayerInteract.pin.interactText.transform.gameObject.SetActive(true);
        //PlayerInteract.pin.interactText.text = interactString;
        PlayerInteract.pin.interactKeyImg.SetActive(true);
        transform.position = new Vector3(transform.position.x, groundOffset, transform.position.z);
        
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
