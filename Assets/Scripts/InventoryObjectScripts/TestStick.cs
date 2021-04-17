﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStick : MonoBehaviour, InventoryItemInterface
{
    public string Name { get; private set; }
    public string ToolTip { get; private set; }
    public GameObject Icon { get; private set; }
    public int Value { get; private set; }


    void Start()
    {
        Name = "Stick";
        ToolTip = "This is a stick, this is a test.";
        Icon = Resources.Load<GameObject>("Assets/Resources/Testing/stick_noun_002_35886.jpg");
        Value = 3;
    }
}
