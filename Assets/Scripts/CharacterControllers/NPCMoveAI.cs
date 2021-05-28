using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMoveAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    private Animator animController;
    private GameObject player;
    private int currentWaypointIndex;
    private bool isAtWaypoint;
    private bool isNearPlayer;
    private float animBlendValue;
    private const float SPEED = 2.0f;
    private const float ANGULAR_SPEED = 120.0f;
    private const float WAIT_TIME = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        SetMotion(SPEED, ANGULAR_SPEED);
        animBlendValue = 1;
        animController.SetFloat(Animator.StringToHash("Blend"), animBlendValue);
        navMeshAgent.stoppingDistance = 1.0f;
        navMeshAgent.SetDestination(waypoints[0].position);
        navMeshAgent.stoppingDistance = .1f;
        player = GameObject.Find("PlayerModel"); 
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAtWaypoint();
        CheckIfNearPlayer();
        animationTransition();
    }

    private void CheckIfAtWaypoint()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
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
            isNearPlayer = true;
            SetMotion(0, 0);
            Quaternion directionToPlayer = Quaternion.Euler(0, 180 / Mathf.PI * Mathf.Atan2(vectorToPlayer.x, vectorToPlayer.z), 0);            transform.rotation = Quaternion.Slerp(transform.rotation, directionToPlayer, 5.0f * Time.deltaTime);
        }else if(!isAtWaypoint)
        {
            isNearPlayer = false;
            SetMotion(SPEED, ANGULAR_SPEED); 
        }
    }

    private void SetMotion(float speed, float angularSpeed)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.angularSpeed = angularSpeed; 
    }

    private void animationTransition()
    {
        if(navMeshAgent.remainingDistance < 1 || isAtWaypoint || isNearPlayer)
        {
            if(animBlendValue >= 0)
            {
                animBlendValue -= Time.deltaTime;
                animController.SetFloat(Animator.StringToHash("Blend"), animBlendValue);
            }
        }
        else if(animBlendValue <= 1)
        {
            animBlendValue += Time.deltaTime;
            animController.SetFloat(Animator.StringToHash("Blend"), animBlendValue);
        }

    }

    private IEnumerator WaitAtWaypoint()
    {
        isAtWaypoint = true;
        SetMotion(0, 0);
        yield return new WaitForSeconds(WAIT_TIME);
        isAtWaypoint = false;
        SetMotion(SPEED, ANGULAR_SPEED);
    }
}
