using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{ 
    public List<InventoryItemInterface> inventoryItem = new List<InventoryItemInterface>(); 

    public enum InvType
    {
        Player,
        TradeMenu,
        EquipMenu
    }

    public InvType inventoryType;
    public void AddItem(InventoryItemInterface item)
    {
        if(inventoryType == InvType.Player && inventoryItem.Count < 24)
        {
            inventoryItem.Add(item);
        }
        else
        {
            Debug.Log("Inventory is full :(");
        }
        if (inventoryType == InvType.TradeMenu && inventoryItem.Count < 1)
        {
            inventoryItem.Add(item);
        }
        else
        {
            Debug.Log("Inventory is full :(");
        }
        if (inventoryType == InvType.EquipMenu && inventoryItem.Count < 4)
        {
            inventoryItem.Add(item);
        }
        else
        {
            Debug.Log("Inventory is full :(");
        }

    }

    public void RemoveItem(InventoryItemInterface item)
    {
        inventoryItem.Remove(item);
    }
 
    public List<InventoryItemInterface> PrintInventory()
    {
        Debug.Log(this.GetHashCode());
        return inventoryItem; 
    }
}
