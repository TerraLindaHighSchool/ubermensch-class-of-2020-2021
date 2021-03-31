using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : InventoryItemInterface
{ 
    ArrayList inventoryItem = new ArrayList(); 

    public void AddItem(InventoryItem item, ArrayList inventoryItem)
    {
        inventoryItem.Add(item);
    }

    public void RemoveItem(InventoryItem item, ArrayList inventoryItem)
    {
        inventoryItem.Remove(item);
    }
 
    public ArrayList PrintInventory(ArrayList inventoryItem)
    {
        return inventoryItem; 
    }
    
}
