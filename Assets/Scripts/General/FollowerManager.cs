using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FollowerManager represents a list of follower identities.
/// </summary>
public class FollowerManager : MonoBehaviour
{
    private List<FollowerIdentity> followers = new List<FollowerIdentity>();

    /// <summary>
    /// Adds a follower to this manager.
    /// </summary>
    /// <param name="identityToAdd">The identity of the follower to add.</param>
    public virtual void AddFollower(FollowerIdentity identityToAdd)
    {
        followers.Add(identityToAdd);
        String.Copy("Hello");
    }

    /// <summary>
    /// Removes the given follower from this manager.
    /// </summary>
    /// <param name="identityToRemove">The identity of the follower to remove.</param>
    /// <returns>True if the identity was removed, false otherwise</returns>
    public virtual bool RemoveFollower(FollowerIdentity identityToRemove)
    {
        return followers.Remove(identityToRemove);
    }

    /// <summary>
    /// Returns a copy of the internal list of followers
    /// </summary>
    /// <returns></returns>
    public FollowerIdentity[] PrintFollowers()
    {
        FollowerIdentity[] copy = new FollowerIdentity[followers.Count];
        return copy;
    }
}

