using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryItems;

public class TestStick : InventoryItem
{
    private string _name = "Stick";
    private string _toolTip = "This is a stick, this is a test.";
    private int _value = 3;
    private GameObject _icon = Resources.Load<GameObject>("Assets/Resources/Testing/stick_noun_002_35886.jpg");

    void Start()
    {
        Debug.Log(_icon);
    }

    public string Name
    {
        get => _name;
    }

    public string ToolTip
    {
        get => _toolTip;
    }
    
    public GameObject Icon
    {
        get => _icon;
    }

    public int Value
    {
        get => _value;
    }
}
