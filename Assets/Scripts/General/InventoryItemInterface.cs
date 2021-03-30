using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemInterface
{
    public interface InventoryItem
    {

        string Name { get; }
        int Value { get; }
        GameObject Icon { get; }
        string ToolTip { get; }

    }

}

public class InventoryManager : InventoryItemInterface
{
    public string inventoryNames; 
    ArrayList inventoryItem = new ArrayList();

    public void AddItem(InventoryItem item, ArrayList inventoryItem)
    {
        inventoryItem.Add(item);
    }

    public void RemoveItem(InventoryItem item, ArrayList inventoryItem)
    {
        inventoryItem.Remove(item);
    }

     // if what is printed should to be the number of items in the inventory:
     // need to add it to the HUD  
     public string PrintInventory(ArrayList inventoryItem)
     {
        foreach (InventoryItem itm in inventoryItem)
        {
            inventoryNames += itm.Name + ", ";
        }
        if(inventoryItem.Capacity == 0)
        {
            inventoryNames = "Inventory is empty"; 
        }
        return inventoryNames;  
     }     
}