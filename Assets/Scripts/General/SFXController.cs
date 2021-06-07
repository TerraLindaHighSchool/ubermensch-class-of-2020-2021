using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioClip[] sfx;

    public AudioClip effectPlay(int soundIndex)
    {
        if (soundIndex <= sfx.Length)
        {
            return sfx[soundIndex];
        }
        else
        {
            Debug.Log("uh oh missing sound effectso");
            return null;
        }
    }
}