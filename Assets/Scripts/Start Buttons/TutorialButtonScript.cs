using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButtonScript : MonoBehaviour
{
    public void ShowTutorial()
	{
		Debug.Log("Tutorial Displayed");
		SceneManager.LoadScene("Tutorial");
	}

}
