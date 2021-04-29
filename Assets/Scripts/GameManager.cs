using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour

{
	public bool gameHasEnded = false;
    
    public void EndGame()
    {
	if(gameHasEnded == false)
	{
	    gameHasEnded = true;
	    Debug.Log("GAME OVER");
	    //Show menu
	}
	
    }

}
