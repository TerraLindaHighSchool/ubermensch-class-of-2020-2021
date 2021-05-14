using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCheck : MonoBehaviour
{
    private List<GameObject> portals;
    private List<GameObject> npcs;
    private List<GameObject> items;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tag: " + other.gameObject.tag);
        switch(other.gameObject.tag)
        {
            case "Portal":
                portals.Add(other.gameObject);
                break;
            case "Friendly NPC":
            case "Non-Friendly NPC":
                npcs.Add(other.gameObject);
                break;
            case "Pick-Up Item":
                items.Add(other.gameObject);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Portal":
                portals.Remove(other.gameObject);
                break;
            case "Friendly NPC":
            case "Non-Friendly NPC":
                npcs.Remove(other.gameObject);
                break;
            case "Pick-Up Item":
                items.Remove(other.gameObject);
                break;
        }
    }

    public List<GameObject> GetPortals()
    {
        return portals;
    }

    public List<GameObject> GetNpcs()
    {
        return npcs;
    }

    public List<GameObject> GetItems()
    {
        return items;
    }
}
