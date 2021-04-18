using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class switchCamera : MonoBehaviour
{
    public GameObject isoCamera;
    public GameObject dialogueCamera;
    public GameObject NPC { private get; set; }
    public bool isInDialogue { private get; set; }


    // Update is called once per frame
    void Update()
    {

        if (isInDialogue && !dialogueCamera.activeInHierarchy)
        {
            dialogueCamera.GetComponent<CinemachineVirtualCamera>().LookAt = NPC.transform;
            Debug.Log(NPC.name);
            isoCamera.SetActive(false);
            dialogueCamera.SetActive(true);

        }
        else if (!isInDialogue && !isoCamera.activeInHierarchy)
        {
            isoCamera.SetActive(true);
            dialogueCamera.SetActive(false);
        }
    }
}
