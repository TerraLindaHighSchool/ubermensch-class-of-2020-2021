using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItemInterface
{
    string ItemName { get; }
    int ItemValue { get; }
    GameObject ItemIcon { get; }
    string ToolTip { get; }

    string GetDisplayName();
    string GetDisplayDescription(); 
}
