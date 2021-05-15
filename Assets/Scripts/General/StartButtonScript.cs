using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public GameObject player;
    public GameObject UI_GameManager;
    public Vector3 spawnPoint;
    public void StartGame()
    {
        Debug.Log("Scene Loaded");
	    SceneManager.LoadScene("HomeBase_UnderSubway");
        DontDestroyOnLoad(Instantiate(player, spawnPoint, new Quaternion(0, 0, 0, 0)));
        DontDestroyOnLoad(Instantiate(UI_GameManager, new Vector3(0, 0, 0), new Quaternion(1, 0, 0, 0)));
    }
}
