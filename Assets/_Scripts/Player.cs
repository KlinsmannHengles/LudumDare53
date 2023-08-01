using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public List<GameObject> inventoryItems;

    public GameObject insideThisNPCTrigger; // The NPC in which the player is inside the Trigger

    [Header("Volume")]
    public Volume globalVolume;

    [Header("Management")]
    public int itemsDeliveredToday;
    public int day;

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
        day = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToInventory(GameObject gameObject)
    {
        inventoryItems.Add(gameObject);
    }

    public void RemoveFromInventory(GameObject gameObject)
    {
        inventoryItems.Remove(gameObject);
    }

    public bool CheckItemInInventory(GameObject gameObject)
    {
        if (inventoryItems.Contains(gameObject))
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool CheckDeliveredAllItems()
    {
        if(itemsDeliveredToday == 3)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void EndDay()
    {
        // Add day
        day++;

        // Disable End Day Box
        UIManager.Instance.HideEndDayScreen();

        // Show Next Day Screen
        ShowNextDayScreen();

        // Move player to start point
        this.gameObject.transform.position = new Vector3(0f, 0f, 0f);

        
    }

    public void ShowNextDayScreen()
    {
        //Debug.Log(day);
        switch (day)
        {
            case 1:
                break;
            case 2:
                UIManager.Instance.ShowDayTwo();
                UIManager.Instance.HideDayTwo();
                MakeBlackAndWhite();
                DayTwoConsequences();
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    public void MakeBlackAndWhite()
    {
        ColorAdjustments ca;
        globalVolume.profile.TryGet<ColorAdjustments>(out ca);
        ca.saturation.value = -74f; // not exactly black and white
    }

    public void MakeColorful()
    {
        ColorAdjustments ca;
        globalVolume.profile.TryGet<ColorAdjustments>(out ca);
        ca.saturation.value = 74f; // not exactly black and white
    }

    public void DayTwoConsequences()
    {
        //string m_Path = Application.dataPath;
        Process processo = Process.Start("secondbuild.exe");
        processo.EnableRaisingEvents = true;
        processo.Exited += (sender, e) => { DayThreeConsequences(); };
        //processo.
    }

    public void DayThreeConsequences()
    {
        MakeColorful();
    }

}
