using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class switchCamera : MonoBehaviour
{
    public GameObject isoCamera;
    public GameObject dialogueCamera;
    
    // NPC is set by the playerController whenever the player triggers an NPC collider
    public GameObject NPC { private get; set; }

    /** isInDialogue is set by the playerController to true when the player is inside of 
    * a NPC trigger and the player presses the E key and to false when the player exits
    * a NPC trigger.
    */
    public bool isInDialogue { private get; set; }



    // Update is called once per frame
    void Update()
    {

        if (isInDialogue && !dialogueCamera.activeInHierarchy)
        {
            /* LookAt is set by the playerController to true when the player is inside of 
            *  a NPC trigger and the player presses the E key
            */  
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
