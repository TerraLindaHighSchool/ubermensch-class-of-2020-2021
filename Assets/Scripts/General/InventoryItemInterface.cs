using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryItem
{ 
    interface IInventoryItem
    {
         string Name { get; set; }
         int Value { get; set; }
         GameObject Icon { get; set; }
         string ToolTip { get; set; }
        
    }   

}