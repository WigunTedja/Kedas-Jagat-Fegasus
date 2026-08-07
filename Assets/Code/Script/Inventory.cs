using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int space = 5;

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Inventory is full");
            return false;
        }
        items.Add(item);
        Debug.Log("Item Picked Up" + item.ItemName);
        foreach (var x in items)
        {
            Debug.Log(x.ToString());
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}
