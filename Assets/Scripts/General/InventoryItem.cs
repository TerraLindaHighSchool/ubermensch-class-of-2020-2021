using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string Name { get; }
    public int Value { get; }
    public GameObject Icon { get; }
    public string ToolTip { get; }

    public InventoryItem(string itemName, int itemValue, GameObject itemIcon, string itemTip)
    {
        itemName = Name;
        itemValue = Value;
        itemIcon = Icon;
        itemTip = ToolTip; 
    }
}
