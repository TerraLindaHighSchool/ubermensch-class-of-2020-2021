using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCamera : MonoBehaviour
{
    public GameObject isoCamera;
    public GameObject dialogueCamera;
    public GameObject playerModel;
    private PlayerController playerController;

    void Start()
    {
        playerController = playerModel.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isInDialogue && !dialogueCamera.activeInHierarchy)
	    {
		isoCamera.SetActive(false);
		dialogueCamera.SetActive(true);

    	}
	    else if(!playerController.isInDialogue && !isoCamera.activeInHierarchy)
	    {
		    isoCamera.SetActive(true);
		    dialogueCamera.SetActive(false);
	    }
    }
}
