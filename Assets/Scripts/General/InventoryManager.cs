using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{ 
    List<InventoryItemInterface> inventoryItem = new List<InventoryItemInterface>(); 

    public void AddItem(InventoryItemInterface item)
    {
        inventoryItem.Add(item);
    }

    public void RemoveItem(InventoryItemInterface item)
    {
        inventoryItem.Remove(item);
    }
 
    public List<InventoryItemInterface> PrintInventory()
    {
        return inventoryItem; 
    }
}
