using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    /*
    int test = 0;

    public void Update()
    {
        test++;
        if(test > 100)
        {
            Debug.Log("Ending Game");
            EndGame();
        }
    }
    */

    public void EndGame()
    {
	    Debug.Log("GAME OVER");

		//Load Menu Scene
	    SceneManager.LoadScene("Fill-inMenu_Knemits");
	
    }	

}
