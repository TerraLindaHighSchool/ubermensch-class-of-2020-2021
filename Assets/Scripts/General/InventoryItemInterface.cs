using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInventoryItem
{
    string Name { get; set; }
    int Value { get; set; }
    //GameObject Icon { get; set; }
    string ToolTip { get; set; }
}


public class FakeInventoryItem : IInventoryItem
{
    public string Name { get; set; } 
    public int Value { get; set; }
    // public GameObject icon;
    public string ToolTip { get; set; }

    public FakeInventoryItem(string itemName, int itemValue, string itemToolTip)
    {
        Name = itemName;
        Value = itemValue;
        ToolTip = itemToolTip;
    }

    FakeInventoryItem GasMask = new FakeInventoryItem("Gas Mask", 20, "Can be equiped to ward off fumes");

}
