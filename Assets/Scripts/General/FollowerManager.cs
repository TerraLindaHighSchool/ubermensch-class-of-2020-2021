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
    /// Returns the FollowerIdentity for the given integer index.
    /// </summary>
    public FollowerIdentity this[int i]
    {
        get { return followers[i]; }
    }

    /// <summary>
    /// Removes the follower at the given index.
    /// </summary>
    /// <param name="index">The index at which to remove the follower.</param>
    /// <returns>
    /// The FollowerIdentity that was removed, 
    /// or null if the follower could not be removed.
    /// </returns>
    public virtual FollowerIdentity RemoveFollowerAt(int index)
    {
        // Do not remove if out of bounds.
        if (index < 0 || index >= followers.Count)
        {
            return null;
        }

        // Remove
        FollowerIdentity at = followers[index];
        followers.RemoveAt(index);
        return at;
    }


    /// <summary>
    /// Returns a copy of the internal list of followers
    /// </summary>
    /// <returns></returns>
    public FollowerIdentity[] PrintFollowers()
    {
        FollowerIdentity[] copy = followers.ToArray();
        return copy;
    }
}

