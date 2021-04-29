using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryManager))]
public class InventoryManagerEditor : Editor
{

    List<InventoryItemInterface> internalInventory;

    public override void OnInspectorGUI()
    {
        InventoryManager actual = serializedObject.targetObject as InventoryManager;
        if (actual != null)
        {
            internalInventory = actual.inventoryItem;
        }

        GUILayout.Label("Inventory Contents");
        if (internalInventory.Count == 0)
        {
            GUILayout.Label("This inventory is empty.");
        }
        else
        {
            foreach (InventoryItemInterface item in internalInventory)
            {
                GUILayout.Label(string.Format("Item: {0}", item.Name));
            }
        }

    }
}
