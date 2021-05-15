using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class StandardInventoryItem : ScriptableObject, InventoryItemInterface
{
    public string _name;
    public string toolTip;
    public Sprite icon;
    public int value;
    public bool questItem;

    public bool QuestItem
    {
        get
        {
            return questItem;
        }
    }
    public string Name { get
        {
            return _name;
        }
    }
    public int Value { get
        {
            return value;
        }
    }
    public Sprite Icon { get
        {
            return icon;
        }
    }
    public string ToolTip { get
        {
            return toolTip;
        }
    }
}
