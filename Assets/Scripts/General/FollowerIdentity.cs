﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Follower Identity", menuName = "Assets/Resources/Followers", order = 1)] 
public class FollowerIdentity : ScriptableObject 
{

    public string Name;
    public int Value;
    public Sprite Icon;
    public string ToolTip;
    public NpcType npcType;
    public int npcStrength;
    public int npcCharisma;
    public int npcConstitution;
    public int npcHealth;
    public GameObject prefab;

    public string GetDisplayName() => Name; 

    public enum NpcType
    {
        Merchant,
        Peaceful,
        Dangerous,
        Aggressive
    }

    public string GetDisplayDescription()
    {
        return "Strength:" + npcStrength + " Charisma:" + npcCharisma + " Consitution:" + npcConstitution; 
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
  