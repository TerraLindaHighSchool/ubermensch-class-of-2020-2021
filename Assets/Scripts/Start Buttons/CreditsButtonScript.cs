using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsButtonScript : MonoBehaviour
{
    public void ShowCredits()
	{
	Debug.Log("Credits Displayed");
	SceneManager.LoadScene("Credits");
	}

}
