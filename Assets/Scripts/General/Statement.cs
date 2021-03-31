using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statement
{

    public string NpcLine;
    public string[] Response;
    public float[] ResponseModifier;
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

