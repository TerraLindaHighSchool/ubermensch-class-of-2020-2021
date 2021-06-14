using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenSupply : MonoBehaviour
{
    public float oxygenSupply;

    private void Start()
    {
        oxygenSupply += Random.Range(-6, 10);
    }
}
