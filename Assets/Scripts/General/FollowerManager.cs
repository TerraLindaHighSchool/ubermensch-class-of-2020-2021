using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public ArrayList followers = new ArrayList();
    
    public void AddFollower(GameObject follower)
    {
        followers.Add(follower);
    }
    
    public void RemoveFollower(GameObject follower)
    {
        followers.Remove(follower);
    }

    public ArrayList PrintFollowers()
    {
        return followers;
    }
}
