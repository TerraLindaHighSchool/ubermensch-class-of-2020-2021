using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class SceneChangeOnTrigger : MonoBehaviour
{
    public SceneChangeLocation scl;
    public gameObject player;

    public void onTriggerEnter(Collider other)
    {
        if(other == player)
        {
            scl.Teleport();
        }
    }
}
