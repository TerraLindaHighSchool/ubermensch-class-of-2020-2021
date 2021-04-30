using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public ArrayList followers = new ArrayList();
    public void AddFollower(GameObject avatar)
    {
        followers.Add(avatar);
    }
    
    public void RemoveFollower(string name)
    {
        followers.Remove(name);
    }

    public ArrayList PrintFollowers()
    {
        return followers;
    }
}
