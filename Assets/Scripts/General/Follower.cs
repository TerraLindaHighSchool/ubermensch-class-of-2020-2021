using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public FollowerIdentity identity;

    public int currentHp;

    public void Awake()
    {
        currentHp = identity.npcHealth;
    }
}
