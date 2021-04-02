using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statement
{
    //what the npc says
    public string NpcLine;
    //what you say
    public string[] Response;
    //how much it changes the relationshhip type
    public float[] ResponseModifier;
    //I think it's which one it sends you to
    public int[] ResponseOutcome;
    
    //I think these were supposed to be different but they look wrong(too small)
    public Statement(string NpcL, string[] R, float[] RM, int[] RO)
    {
        NpcLine = NpcL;
        Response = R;
        ResponseModifier = RM;
        ResponseOutcome = RO;
    }
    
}

