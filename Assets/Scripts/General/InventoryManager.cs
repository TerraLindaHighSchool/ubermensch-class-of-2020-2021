using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{ 
    ArrayList inventoryItem = new ArrayList(); 

    public void AddItem(InventoryItem item)
    {
        inventoryItem.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        inventoryItem.Remove(item);
    }
 
    public ArrayList PrintInventory(ArrayList inventoryItem)
    {
        return inventoryItem; 
    }
}
