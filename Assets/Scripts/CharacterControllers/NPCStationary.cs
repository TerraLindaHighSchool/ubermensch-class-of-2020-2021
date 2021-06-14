using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPCStationary : MonoBehaviour
{
    private Animator animController;
    private GameObject player;
    private float animBlendValue;
    private const float SPEED = 2.0f;
    private const float ANGULAR_SPEED = 120.0f;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            animController = GetComponent<Animator>();
            animBlendValue = 1;
            animController.SetFloat(Animator.StringToHash("Blend"), animBlendValue);
            player = GameObject.Find("PlayerModel");
        }
        catch(UnassignedReferenceException e)
        {
            Debug.Log("SHOULD BE IN HOME BASE");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "HomeBase_UnderSubway")
        {
            CheckIfNearPlayer();
        }
    }

    private void CheckIfNearPlayer()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        if (vectorToPlayer.magnitude < 2)
        {
            Quaternion directionToPlayer = Quaternion.Euler(0, 180 / Mathf.PI * Mathf.Atan2(vectorToPlayer.x, vectorToPlayer.z), 0);            transform.rotation = Quaternion.Slerp(transform.rotation, directionToPlayer, 5.0f * Time.deltaTime);
        }

    }

}
