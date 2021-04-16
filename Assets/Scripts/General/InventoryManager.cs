using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager 
{ 
    ArrayList inventoryItem = new ArrayList(); 

    public void AddItem(InventoryItemInterface item)
    {
        inventoryItem.Add(item);
    }

    public void RemoveItem(InventoryItemInterface item)
    {
        inventoryItem.Remove(item);
    }
 
    public ArrayList PrintInventory(ArrayList inventoryItem)
    {
        return inventoryItem; 
    }
}
