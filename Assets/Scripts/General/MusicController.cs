using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] tracks;//makes track array

    public AudioClip TrackSwitch(int trackIndex)
    {
        if (trackIndex <= tracks.Length)
        {
            return tracks[trackIndex];//returns the requested track
        }
        else
        {
            Debug.Log("oh no no musico");//returns error if the track does not exist
            return null;
        }
    }

}
