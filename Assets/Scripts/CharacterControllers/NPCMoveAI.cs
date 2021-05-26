﻿using System.Collections;
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
    private const float SPEED = 2.0f;
    private const float ANGULAR_SPEED = 120.0f;
    private const float WAIT_TIME = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        SetMotion(SPEED, ANGULAR_SPEED);
        animController.SetFloat(Animator.StringToHash("Blend"), 1);
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
        isAtWaypoint = true;
        SetMotion(0, 0);
        animController.SetFloat(Animator.StringToHash("Blend"), 0);
        yield return new WaitForSeconds(WAIT_TIME);
        isAtWaypoint = false;
        SetMotion(SPEED, ANGULAR_SPEED);
        animController.SetFloat(Animator.StringToHash("Blend"), 1);
    }
}
