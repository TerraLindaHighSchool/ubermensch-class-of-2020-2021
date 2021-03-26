using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryItems
{ 
    interface InventoryItem
    {

        string Name { get; }
        int Value { get; }
        GameObject Icon { get; }
        string ToolTip { get; }
        
    }  
}