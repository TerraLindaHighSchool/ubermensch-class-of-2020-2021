using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerTrader : MonoBehaviour
{
    FollowerManager playerManager;
    FollowerManager gameManager;
    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FollowerManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FollowerManager>();
    }
    
    private List<GameObject> createdNPCs = new List<GameObject>();
    Vector3[] partyPositions = new[] { new Vector3(30f, -1f, 13f), new Vector3(30f, -1f, 12f), new Vector3(30f, -1f, 11f), new Vector3(30f, -1f, 10f) };

    public void MoveFollower(int Num, bool isPlayerMenu)
    {
        if (isPlayerMenu)
        {
            gameManager.AddFollower(playerManager[Num]);
            playerManager.RemoveFollowerAt(Num);
        }else
        {
            playerManager.AddFollower(gameManager[Num]);
            gameManager.RemoveFollowerAt(Num);
        }
    }
    void PartyInTheHomeBase()
       {
        foreach(GameObject GO in createdNPCs)
        {
            Destroy(GO);
        }
        createdNPCs.Clear();

        FollowerIdentity[] followersInHomeBase = gameManager.PrintFollowers();
        for(int i = 0; i < followersInHomeBase.Length; i++)
        {
            GameObject NewNPC = Instantiate(followersInHomeBase[i].prefab , partyPositions[i], Quaternion.identity);
            createdNPCs.Add(NewNPC);
        }
    }
}
