using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItemInterface
{
    string Name { get; }
    int Value { get; }
    GameObject Icon { get; }
    string ToolTip { get; }

}
