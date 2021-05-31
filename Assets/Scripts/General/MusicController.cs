using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] music;
    AudioSource audioPlayer;

    private void Awake()
    {
        audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        ChangeVolume();
    }

    public void TrackSwitch(int trackIndex, AudioClip[] tracks)
    {
        if (trackIndex <= tracks.Length)
        {
            float store = audioPlayer.volume;
            while (audioPlayer.volume > 0)
            {
                audioPlayer.volume -= 0.00005f;
            }
            audioPlayer.Stop();
            audioPlayer.volume = store;

            //plays the requested track
            //audioPlayer.clip = tracks[trackIndex];
            audioPlayer.Play();
        }
        else
        {
            Debug.Log("oh no no musico");//returns error if the track does not exist
        }
    }

    public void ChangeVolume()
    {
        if (Input.GetKey(KeyCode.Equals) || Input.GetKey(KeyCode.Plus))
        {
            audioPlayer.volume += 0.005f;
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            audioPlayer.volume -= 0.005f;
        }
    }

}
