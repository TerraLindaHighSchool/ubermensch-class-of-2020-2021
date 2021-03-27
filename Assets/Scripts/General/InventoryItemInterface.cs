using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryItems
{ 
    interface InventoryItem
    {

        string Name { get; }
        int Value { get; }
       // GameObject Icon { get; }
        string ToolTip { get; }
        
    }
    class FakeInventoryItem : InventoryItem
    {
        public string Name { get; }
        public int Value { get; }
        public string ToolTip { get; }

        public FakeInventoryItem(string itemName, int itemValue, string itemTip)
        {
            Name = itemName;
            Value = itemValue;
            ToolTip = itemTip;
        }

        
    }

    class InventoryManager
    {
        int numSoap;
        int numFood;
        int numShield;
        int numJewelry;
        int numMedicine;
        int numShiv;
        
        ArrayList inventoryItem = new ArrayList();

        public void AddItem(InventoryItem item)
        {
            inventoryItem.Add(item);
        }

        public void RemoveItem(InventoryItem item)
        {
            inventoryItem.Remove(item);
        }

        // if what is printed should to be the number of items in the inventory:
        // need to add it to the HUD once it's made 
        public int PrintInventory()
        {
            return inventoryItem.Capacity; 
        }

        // if what is printed should to be the name of the items in the inventory:
        // need to add to the HUD once it's made - that shouold be simple though
        // Specific items can be added (like a gas mask, but the specific ones weren't given) 
        public string PrintInventoryItem()
        {
            string inventorySummary;

            foreach(InventoryItem itm in inventoryItem)
            {
                if(itm.Name.Equals("Soap"))
                {
                    numSoap++; 
                }
                if(itm.Name.Equals("Food"))
                {
                    numFood++; 
                }
                if (itm.Name.Equals("Shield"))
                {
                    numShield++; 
                }
                if(itm.Name.Equals("Jewelry"))
                {
                    numJewelry++; 
                }
                if(itm.Name.Equals("Medicine"))
                {
                    numMedicine++; 
                }
                if(itm.Name.Equals("Shiv"))
                {
                    numShiv++; 
                }

            }
            inventorySummary = "Soap: " + numSoap + "Food: " + numFood + "Shields: " + numShield + "Jewelry: " + numJewelry + "Medicine" + numMedicine + "Shiv" + numShiv;
            return inventorySummary; 
        }

        public void Start()
        {
            FakeInventoryItem Soap = new FakeInventoryItem("Soap", 20, "Spend Money");
            inventoryItem.AddItem(Soap);
        }

    }
       
}