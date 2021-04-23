using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public Object[] scenes = new Object[10];
    public GameObject player;
    public void SceneLoader(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
        DontDestroyOnLoad(player);
    }
}
