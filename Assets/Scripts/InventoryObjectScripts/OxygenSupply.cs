using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenSupply : MonoBehaviour
{
    public float oxygenSupply;

    private void Start()
    {
        oxygenSupply += Random.Range(-6, 6);
        if(oxygenSupply < 0)
        {
            oxygenSupply = 0;
        }

        if(oxygenSupply > 100)
        {
            oxygenSupply = 100;
        }
    }
}
