using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject panel;

    public GameObject warning; // item already received warning

    public GameObject dayOne;
    public GameObject dayTwo;
    public GameObject dayThree;

    public GameObject endDay;

    public GameObject[] buttons;
    public Image[] buttonsImages;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HideDayOne();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowEndDayScreen()
    {
        endDay.SetActive(true);
        endDay.GetComponent<CanvasGroup>().DOFade(255f, 2f).SetEase(Ease.InExpo);
    }

    public void HideEndDayScreen()
    {
        endDay.SetActive(false);
    }

    public void ShowDayTwo()
    {
        dayTwo.SetActive(true);
    }

    public void HideDayTwo()
    {
        dayTwo.GetComponent<CanvasGroup>().DOFade(0f, 4f).SetEase(Ease.InExpo).onComplete = DisableDayTwo;
    }

    // Support for HideDayTwo
    public void DisableDayTwo()
    {
        dayTwo.SetActive(true);
    }

    public void HideDayOne()
    {
        dayOne.GetComponent<CanvasGroup>().DOFade(0f, 4f).SetEase(Ease.InExpo).onComplete = DisableDayOne;
    }

    public void DisableDayOne()
    {
        dayOne.SetActive(false);
    }

    public void ShowDeliveryPanel()
    {
        panel.SetActive(true);
    }

    public void HideDeliveryPanel()
    {
        panel.SetActive(false);
    }

    public void ShowWarningItemAlreadyReceived()
    {
        warning.SetActive(true);
        warning.GetComponent<CanvasGroup>().DOFade(0f, 2f).SetEase(Ease.InExpo).onComplete = DisableWarningItemAlreadyReceived;
    }

    public void DisableWarningItemAlreadyReceived()
    {
        //warning.GetComponent<CanvasGroup>().DOFade(255f, 0.001f);
        warning.GetComponent<CanvasGroup>().alpha = 255f;
        warning.SetActive(false);
    }

    // add item to button
    public void AddItemToButtonInDeliveryPanel()
    {
        int index = 0;
        foreach (GameObject item in Player.Instance.inventoryItems)
        {
            buttons[index].GetComponent<ButtonBehaviour>().item = item;
            index++;
        }
    }

    // clear the items in the buttons from Panel Inventory
    public void ClearItemsInButtonsInDeliveryPanel()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<ButtonBehaviour>().item = null;
        }
    }

    // put the items images in the buttons children images
    public void ShowInventoryItemsImagesInDeliveryPanel()
    {
        int index = 0;
        foreach (GameObject item in Player.Instance.inventoryItems)
        {
            buttonsImages[index].sprite = item.GetComponent<SpriteRenderer>().sprite;
            index++;
        }
    }

    // clear the items images in the delivery panel
    public void ClearInventoryItemsImagesInDeliveryPanel()
    {
        foreach (Image itemSprite in buttonsImages)
        {
            itemSprite.sprite = null;
        }
    }



    // Happens when the player click on button to deliver an item
    public void DeliverItem(GameObject gameObject)
    {
        if (gameObject == null)
        {
            Debug.Log("There is not a item attached to this button");
            return;
        }

        // add the item to the NPC inventory
        Player.Instance.insideThisNPCTrigger.GetComponent<NPCBehaviour>().AddItemToInventory(gameObject);

        // states that the NPC already received an item today
        Player.Instance.insideThisNPCTrigger.GetComponent<NPCBehaviour>().itemReceivedToday = true;
        
        // remove item from Player inventory
        Player.Instance.RemoveFromInventory(gameObject);

        // disable the item
        gameObject.SetActive(false);

        // Hide Delivery Panel
        HideDeliveryPanel();
        
        // Clear Panel
        ClearInventoryItemsImagesInDeliveryPanel();
        ClearItemsInButtonsInDeliveryPanel();

        // Count
        Player.Instance.itemsDeliveredToday += 1;

        // Show End Day Box if it was the last delivery of this day
        if (Player.Instance.CheckDeliveredAllItems())
        {
            ShowEndDayScreen();
        }

    }
}
