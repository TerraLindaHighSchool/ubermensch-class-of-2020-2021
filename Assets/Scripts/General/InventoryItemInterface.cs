using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItemInterface
{
    string Name { get; }
    string Value { get; }
    Sprite Icon { get; }
    string ToolTip { get; }
}
