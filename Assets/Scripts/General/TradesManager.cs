using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradesManager : MonoBehaviour
{
    public int itemCost;
    public InventoryItemInterface itemTraded;
    public void UpdateUI()
    {
        itemCost = itemTraded.Value;
    }

    public void CheckOut()
    {
        //if(player.soap >= itemCost)
        //{
        //      InventoryManager.AddItem(itemTraded);
        //      player.soap -= itemCost
        //}
    }

    public void SellItem()
    {
        //InventoryManager.RemoveItem(itemTraded);
        //player.soap += itemCost;
    }
}
