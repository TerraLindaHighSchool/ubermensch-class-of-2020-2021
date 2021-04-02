using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCamera : MonoBehaviour
{
    public GameObject isoCamera;
    public GameObject dialogueCamera;
    public GameObject npc;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (npc && !dialogueCamera.activeInHierarchy)
        //{
            //isoCamera.SetActive(false);
            //dialogueCamera.SetActive(true);

            //Allow time for the camera to blend before enabling the UI
            //StartCoroutine(Switch());
        //}
        if(npc && !isoCamera.activeInHierarchy)
        {
            isoCamera.SetActive(true);
            dialogueCamera.SetActive(false);
            npc.SetActive(false);
        }
    }
	IEnumerator Switch()
    {
        yield return new WaitForSeconds(0.25f);
        
    }
}
