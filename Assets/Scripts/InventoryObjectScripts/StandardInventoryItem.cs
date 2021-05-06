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
    /*
    public TestStick()
    {
        Name = "Stick";
        ToolTip = "This is a stick, this is a test.";
        Icon = Resources.Load<Sprite>("Assets/Resources/Icons/stick_noun_002_35886.jpg");
        Value = 3;
    }
    */
}
