using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Scene Reloaded");
	//Replace Seattle with the last scene the player was on
	SceneManager.LoadScene("Seattle");

    }
}
