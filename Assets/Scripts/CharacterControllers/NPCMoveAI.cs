using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMoveAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
   // public Animator animController;
    private GameObject player;
    private int currentWaypointIndex;
    private bool isAtWaypoint;
    private const float SPEED = 2.0f;
    private const float ANGULAR_SPEED = 120.0f;
    private const float WAIT_TIME = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // start walking animation
        SetMotion(SPEED, ANGULAR_SPEED);
        navMeshAgent.SetDestination(waypoints[0].position);
        player = GameObject.Find("PlayerModel"); 
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAtWaypoint();
        CheckIfNearPlayer(); 
    }

    private void CheckIfAtWaypoint()
    {
        if(navMeshAgent.stoppingDistance > navMeshAgent.remainingDistance)
        {
            StartCoroutine(WaitAtWaypoint());
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    private void CheckIfNearPlayer()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        if (vectorToPlayer.magnitude < 2)
        {
            SetMotion(0, 0);
            Quaternion directionToPlayer = Quaternion.Euler(0, 180 / Mathf.PI * Mathf.Atan2(vectorToPlayer.x, vectorToPlayer.z), 0);            transform.rotation = Quaternion.Slerp(transform.rotation, directionToPlayer, 5.0f * Time.deltaTime);
        }else if(! isAtWaypoint)
        {
            SetMotion(SPEED, ANGULAR_SPEED); 
        }
    }

    private void SetMotion(float speed, float angularSpeed)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.angularSpeed = angularSpeed; 
    }

    private IEnumerator WaitAtWaypoint()
    {
        //run idle animation before yield
        // animController.SetBool("isWalking", false); 
        isAtWaypoint = true;
        SetMotion(0, 0); 
        yield return new WaitForSeconds(WAIT_TIME);
        // start walking animation
        // animController.SetBool("isWalking", true);
        isAtWaypoint = false;
        SetMotion(SPEED, ANGULAR_SPEED);
    }
}
