using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAIMove : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public Animator animController;
    public GameObject player;
    private int currentWaypointIndex;
    private bool isAtWaypoint;
    private const float SPEED = 2.0f;
    private const float ANGULAR_SPEED = 120.0f;
    private const float WAIT_TIME = 3.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckIfAtWaypoint()
    {

    }

    private void CheckIfInConversationOrNearPlayer()
    {

    }

    private void SetMotion(float speed, float angularSpeed)
    {

    }

    //private IEnumerator WaitAtWaypoint()
    //{

    //}
}
