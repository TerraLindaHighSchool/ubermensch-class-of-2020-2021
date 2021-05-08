using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerIdentity : ScriptableObject, InventoryItemInterface
{
    public string Name { get; set; }
    public int Value { get; set; }
    public Sprite Icon { get; set; }
    public string ToolTip { get; set; }
    public int npcStrength { get; set; }
    public int npcCharisma { get; set; }
    public int npcConstitution { get; set; }

    public FollowerIdentity(string _name, int _value, Sprite _icon, string _tip, int _strength, int _charisma, int _constitution)
    {
        Name = _name;
        Value = _value;
        Icon = _icon;
        ToolTip = _tip;
        npcStrength = _strength;
        npcCharisma = _charisma;
        npcConstitution = _constitution; 
    }

    public string GetDisplayName()
    {
        return "The Follower's Name is: " + Name; 
    }

    public string GetDisplayDescription()
    {
        return Name + "'s Strength is: " + npcStrength + " Their Charisma is: " + npcCharisma + " And their Consitution is: " + npcConstitution; 
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
  