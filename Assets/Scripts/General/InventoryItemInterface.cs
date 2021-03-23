using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemInterface : MonoBehaviour
{
    interface IInventoryItem
    {
        string name { get; set; }
        int value { get; set; }
        GameObject icon {get; set;}
        string toolTip { get; set; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
