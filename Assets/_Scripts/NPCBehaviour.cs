using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    private bool insideTrigger = false; // to check if the player is inside the enemy trigger

    public GameObject[] inventory; // limited to 3 items

    public bool itemReceivedToday = false; // if the NPC already received an item today

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 
        if (Input.GetKeyDown("q") && insideTrigger)
        {
            if (itemReceivedToday == false)
            {
                UIManager.Instance.ShowDeliveryPanel();
                UIManager.Instance.ShowInventoryItemsImagesInDeliveryPanel();
                UIManager.Instance.AddItemToButtonInDeliveryPanel();
            } else
            {
                UIManager.Instance.ShowWarningItemAlreadyReceived();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the player is inside the NPC range it enable interaction
        if (collision.tag == "Player")
        {
            insideTrigger = true;

            Player.Instance.insideThisNPCTrigger = this.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // hide the delivery panel when the player goes out the NPC trigger
        if (collision.tag == "Player")
        {
            insideTrigger = false;

            Player.Instance.insideThisNPCTrigger = null;

            UIManager.Instance.HideDeliveryPanel();
            UIManager.Instance.ClearInventoryItemsImagesInDeliveryPanel();
            UIManager.Instance.ClearItemsInButtonsInDeliveryPanel();
        }
    }

    // add an item to this NPC inventory
    public void AddItemToInventory(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;

                return;
            }
        }
    }

}
