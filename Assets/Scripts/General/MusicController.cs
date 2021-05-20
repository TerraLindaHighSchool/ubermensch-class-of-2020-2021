using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] tracks;//makes track array
    AudioSource audioPlayer;
    private void Awake()
    {
        audioPlayer = this.GetComponent<AudioSource>();
        TrackSwitch(2);
    }

    private void Update()
    {
        ChangeVolume();
    }

    public void TrackSwitch(int trackIndex)
    {
        if (trackIndex <= tracks.Length)
        {
            float store = this.gameObject.GetComponent<AudioSource>().volume;
            while (this.gameObject.GetComponent<AudioSource>().volume > 0)
            {
                this.gameObject.GetComponent<AudioSource>().volume -= 0.05f;
            }
            this.gameObject.GetComponent<AudioSource>().Stop();
            this.gameObject.GetComponent<AudioSource>().volume = store;

            //plays the requested track
            this.gameObject.GetComponent<AudioSource>().clip = tracks[trackIndex];
            this.gameObject.GetComponent<AudioSource>().Play();
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
