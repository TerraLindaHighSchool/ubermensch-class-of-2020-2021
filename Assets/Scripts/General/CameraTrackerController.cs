using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraTrackerController : MonoBehaviour
{
	public GameObject player;
	public GameObject cameraTracker;
	

    // Start is called before the first frame update
    void Start()
    {
        cameraTracker.transform.rotation = Quaternion.Euler(Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        cameraTracker.transform.position = player.transform.position + new Vector3(0,0.5f,0);

    }

}
