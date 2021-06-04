using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCredits : MonoBehaviour
{
       public void ExitCreditsScene()
	{
	
	if(Input.GetKeyDown(KeyCode.W))
		Debug.Log("Exit Credits");
		SceneManager.LoadScene("StartMenu");
	}
}
