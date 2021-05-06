using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransferManager : MonoBehaviour
{
    public bool Transfer(InventoryManager sender, InventoryManager reciever, InventoryItemInterface item)
    {
        bool successful = false;
        if (sender.inventoryType == InventoryManager.InvType.Player && reciever.inventoryType == InventoryManager.InvType.Player)
        {
            if(reciever.soap >= item.Value)
            {
                sender.RemoveItem(item);
                reciever.AddItem(item);
                sender.soap += item.Value;
                reciever.soap -= item.Value;
                successful = true;
            }
            else
            {
                Debug.Log("Trade Failed");
                successful = false;
            }
        }
        else
        {
            sender.RemoveItem(item);
            reciever.AddItem(item);
            successful = true;
        }
        HUDController.HUDLoader();
        return successful;
    }
}
