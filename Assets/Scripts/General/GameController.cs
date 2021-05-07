using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{

    
    public void EndGame()
    {
	    Debug.Log("GAME OVER");

		//Load Menu Scene
	    SceneManager.LoadScene("TestMenu_Knemits");
	
    }

}
