using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStick : MonoBehaviour, InventoryItemInterface
{
    public string ItemName { get; private set; }
    public string ToolTip { get; private set; }
    public GameObject ItemIcon { get; private set; }
    public int ItemValue { get; private set; }

    public string GetDisplayName()
    {
        return ItemName; 
    }

    public string GetDisplayDescription()
    {
        return ToolTip; 
    }

    void Start()
    {
        ItemName = "Stick";
        ToolTip = "This is a stick, this is a test.";
        ItemIcon = Resources.Load<GameObject>("Assets/Resources/Icons/stick_noun_002_35886.jpg");
        ItemValue = 3;
    }
}
