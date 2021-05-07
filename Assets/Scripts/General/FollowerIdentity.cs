using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerIdentity : ScriptableObject, InventoryItemInterface
{
    public string ItemName { get; set; }
    public int ItemValue { get; set; }
    public GameObject ItemIcon { get; set; }
    public string ToolTip { get; set; }
    public int npcStrength { get; set; }
    public int npcCharisma { get; set; }
    public int npcConstitution { get; set; }

    public FollowerIdentity(string _name, int _value, GameObject _icon, string _tip, int _strength, int _charisma, int _constitution)
    {
        ItemName = _name;
        ItemValue = _value;
        ItemIcon = _icon;
        ToolTip = _tip;
        npcStrength = _strength;
        npcCharisma = _charisma;
        npcConstitution = _constitution; 
    }

    public string GetDisplayName()
    {
        return "The Follower's Name is: " + ItemName; 
    }

    public string GetDisplayDescription()
    {
        return ItemName + "'s Strength is: " + npcStrength + " Their Charisma is: " + npcCharisma + " And their Consitution is: " + npcConstitution; 
    }

    // get methods for the PlayerController class 
    public int GetFollowerStrength()
    {
        return npcStrength; 
    }

    public int GetFollowerCharisma()
    {
        return npcCharisma;
    }

    public int GetFollowerConstitution()
    {
        return npcConstitution;
    }


}
  