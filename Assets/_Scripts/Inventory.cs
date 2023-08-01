using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public List<GameObject> objects;

    public void AddToInventory(GameObject gameObject)
    {
        objects.Add(gameObject);
    }

    public void RemoveFromInventory(GameObject gameObject)
    {
        if (objects.Contains(gameObject))
        {
            objects.Remove(gameObject);
        } else
        {
            Debug.Log("The GameObject " + gameObject + " is not in the Inventory List");
        }
    }

}
