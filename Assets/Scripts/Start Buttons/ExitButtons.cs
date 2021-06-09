using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/***************************************************** 
 * Attached to canvas of credits and tutorial scenes
 *****************************************************/
public class ExitButtons : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
	    {
		    SceneManager.LoadScene("StartMenu");
	    }
    }
}
