using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{ 
    public List<InventoryItemInterface> inventoryItem = new List<InventoryItemInterface>();
    public StandardInventoryItem[] startingInventory;
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

    public void Awake()
    {
        foreach(StandardInventoryItem i in startingInventory)
        {
            AddItem(i);
        }
    }

    private bool needsTRANS5 = true;

    public bool AddItem(InventoryItemInterface item)
    {
        if(inventoryType == InvType.Player && inventoryItem.Count <= 23)
        {
            inventoryItem.Add(item);
            playerFull = false;
            return true;
        }
        else
        {
            playerFull = true;
        }

        if (inventoryType == InvType.TradeMenu && inventoryItem.Count < 1)
        {
            inventoryItem.Add(item);
            tradeFull = false;
            return true;
        }
        else
        {
            tradeFull = true;
        }

        if (inventoryType == InvType.EquipMenu && inventoryItem.Count < 4)
        {
            if (item.Name == "Gas Mask" && needsTRANS5 && GameObject.Find("GameManager").GetComponent<TutorialController>().slidePos()[1] == 5)
            {
                GameObject.Find("GameManager").GetComponent<TutorialController>().advanceSlide();
                needsTRANS5 = false;
            }
            inventoryItem.Add(item);
            equipFull = false;
            return true;
        }
        else
        {
            equipFull = true;
        }

        if(playerFull && tradeFull && equipFull)
        {
            Debug.Log("Inventory Full :(");
            return false;
        }

        Debug.Log("If you're seeing this something has gone very wrong in the Inventory Manager.");
        return false;
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
