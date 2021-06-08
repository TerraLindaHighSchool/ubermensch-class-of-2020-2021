using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraTrackerController : MonoBehaviour
{
	public GameObject player;
	

    // Start is called before the first frame update
    private void Awake()
    {
        SceneManager.activeSceneChanged += RotateCameraTargetOnSceneChange;
        try
        {
            player.GetComponent<Transform>();
        }
        catch(UnassignedReferenceException e)
        {
            player = GameObject.Find("pTorus1");
        }
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0,0.5f,0);
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= RotateCameraTargetOnSceneChange;
    }

    
    private void RotateCameraTargetOnSceneChange(Scene current, Scene next)
    {
        transform.rotation = Quaternion.Euler(Vector3.up);
    }

}
