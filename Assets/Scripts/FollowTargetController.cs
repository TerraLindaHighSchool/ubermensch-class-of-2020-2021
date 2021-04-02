using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetController : MonoBehaviour
{
	public GameObject player;
	public GameObject followTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        followTarget.transform.position = player.transform.position + new Vector3(0,0.5f,0);
	
    }

}
