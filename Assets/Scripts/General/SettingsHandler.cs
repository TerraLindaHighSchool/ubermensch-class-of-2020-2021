using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsHandler : MonoBehaviour
{
    public string menu_scene = "change me!";

    public void OnVolumeSliderUpdate(float value){
        AudioListener.volume = value;
    }

    public void OnExitGameButtonPressed(){
        SceneManager.LoadScene(menu_scene);
    }


    /*
    names of controls:

    forward
    backward
    left
    right
    inventory
    follower
    interact

    these will be passed through the "control_name" variable
    */
    public void AssignKey(string control_name){
        //Code for assigning keys goes here
    }
}
