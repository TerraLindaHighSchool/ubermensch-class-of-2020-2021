using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{ 
    public List<InventoryItemInterface> inventoryItem = new List<InventoryItemInterface>();
    public int soap;
    public enum InvType
    {
        Player,
        TradeMenu,
        EquipMenu
    }

    private bool playerFull;
    private bool tradeFull;
    private bool equipFull;

    public InvType inventoryType;
    public void AddItem(InventoryItemInterface item)
    {
        if(inventoryType == InvType.Player && inventoryItem.Count < 24)
        {
            inventoryItem.Add(item);
            playerFull = false;
        }
        else
        {
            playerFull = true;
        }
        if (inventoryType == InvType.TradeMenu && inventoryItem.Count < 1)
        {
            inventoryItem.Add(item);
        }
        else
        {
            tradeFull = true;
        }
        if (inventoryType == InvType.EquipMenu && inventoryItem.Count < 4)
        {
            inventoryItem.Add(item);
        }
        else
        {
            equipFull = true;
        }
        if(playerFull && tradeFull && equipFull)
        {
            Debug.Log("Inventory Full :(");
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
