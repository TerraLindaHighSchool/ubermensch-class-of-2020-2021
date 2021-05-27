using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Follower Identity", menuName = "Assets/Resources/Followers", order = 1)] 
public class FollowerIdentity : ScriptableObject 
{

    public string Name;
    public int Value;
    public Sprite Icon;
    public string ToolTip;
    public bool merchant;
    public int npcStrength;
    public int npcCharisma;
    public int npcConstitution;
    public GameObject prefab;

    public string GetDisplayName() => Name; 
   

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
  