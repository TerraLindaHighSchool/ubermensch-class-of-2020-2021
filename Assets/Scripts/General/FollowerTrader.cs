using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerTrader : MonoBehaviour
{
    public FollowerManager playerManager;
    public FollowerManager gameManager;
    Vector3[] partyPositions = new[] { new Vector3(30f, -1f, 13f), new Vector3(30f, -1f, 12f), new Vector3(30f, -1f, 11f), new Vector3(30f, -1f, 10f) };
    //private Vector3[] partyPositions = new Vector3[4];
    //partyPositions.Add( )

    void MoveFollower(int Num, bool isPlayerMenu)
    {
        if (isPlayerMenu)
        {
            gameManager.followers.Add(playerManager.followers[Num]);
            playerManager.followers.RemoveAt(Num);
        }else
        {
            playerManager.followers.Add(gameManager.followers[Num]);
            gameManager.followers.RemoveAt(Num);
        }
    }
    /************************************** Above method but split into 2 methods
    void RetireFollower(int Num)
    {
        gameManager.followers.Add(playerManager.followers[Num]);
        playerManager.followers.RemoveAt(Num);
    }

    void ActivateFollower(int Num)
    {
        playerManager.followers.Add(gameManager.followers[Num]);
        gameManager.followers.RemoveAt(Num);
    }
    ******************************************************************/
    void PartyInTheHomeBase()
    {
        Instantiate( , partyPositions[1], );
        Instantiate( , partyPositions[1], );
        Instantiate( , partyPositions[1], );
        Instantiate( , partyPositions[1], );
    }
}
