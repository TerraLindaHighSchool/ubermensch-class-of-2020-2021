﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] tracks;//makes track array
    AudioSource audioPlayer;
    private void Start()
    {
        audioPlayer = this.GetComponent<AudioSource>();
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
            StartCoroutine("Loop");
        }
        else
        {
            Debug.Log("oh no no musico");//returns error if the track does not exist
        }
    }

    public IEnumerator Loop()
    {
        AudioClip start = this.gameObject.GetComponent<AudioSource>().clip;
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(this.gameObject.GetComponent<AudioSource>().clip.length);
        if (start != this.gameObject.GetComponent<AudioSource>().clip) { yield break; }
        StartCoroutine("Loop");
    }

    public void ChangeVolume()
    {
        if (Input.GetKey(KeyCode.Equals))
        {
            audioPlayer.volume += 0.005f;
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            audioPlayer.volume -= 0.005f;
        }
    }

}
