using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public List<FollowerInterface.Follower> followers = new List<FollowerInterface.Follower>();
    public void AddFollower(GameObject avatar)
    {
        followers.Add(avatar);
    }

    public void RemoveFollower(string name)
    {
        followers.Remove(FollowerInterface.name);
    }

  /*  public InventoryItemInterface.InventoryItem[] PrintFollowers()
    {
        
    }*/

}
