using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject item;

    public void DeliverButton()
    {
        UIManager.Instance.DeliverItem(item);
    }

}
