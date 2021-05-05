using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransferManager : MonoBehaviour
{
    public static GameObject selectedButton;
    public GameObject selectedButtonID;

    public bool Transfer(InventoryManager sender, InventoryManager reciever, InventoryItemInterface item)
    {
        return true;
    }
}
