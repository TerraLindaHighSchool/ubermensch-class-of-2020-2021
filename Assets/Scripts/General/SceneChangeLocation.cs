using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Media;
using UnityEngine;

public class SceneChangeLocation : MonoBehaviour
{
    public string sceneTarget;
    public Vector3 destinationPosition;
    public gameObject player;
    
    public void teleport()
    {
        player.gameObject.position = destinationPosition;
        DontDestroyOnLoad(player);
    }
}
