using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statement : MonoBehaviour
{
    //what the npc says (in my csv it was "Statement")
    public string NpcLine;
    //what you say (in my csv it was "Option 1" or "Option 2" but there can probably be as many options as you want)
    public string[] Response;
    //how much it changes the relationshhip type (in my csv it was "Rep 1" or "Rep 2" same thing as for response)
    public float[] ResponseModifier;
    //I think it's which one it sends you to (in my csv it was "Option 1 Link" or "Option 2 Link" same thing as response and responsemodifier)
    public int[] ResponseOutcome;
    
    //I think these were supposed to be different but they look wrong(too small)
    //no it's probably right
    //it's an array so it actually already accounts for all the shenanigans
    public Statement(string NpcL, string[] R, float[] RM, int[] RO)
    {
        NpcLine = NpcL;
        Response = R;
        ResponseModifier = RM;
        ResponseOutcome = RO;
    }
    
}

