using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    public string inventory_Char = "i";
    public string follower_Char = "u";
    public string bio_Char = "o";
    public string interact_Char = "e";

    public GameObject inventory;
    public GameObject follower;
    public GameObject interact;
    public GameObject bio;

    private GameObject activeButtonText;
    private string activeString;
    private bool isListening;

    public void OnVolumeSliderUpdate(float value){
        AudioListener.volume = value;
    }

    public void OnExitGameButtonPressed()
    {
        GameObject.Find("GameManager").GetComponent<HUDController>().noActiveHud();
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("UI_Manager"));
        SceneManager.LoadScene("StartMenu");
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

        isListening = true;
        activeString = control_name;

        switch (control_name)
        {
            case "inventory":
                activeButtonText = inventory;
                break;
            case "follower":
                activeButtonText = follower;
                break;
            case "interact":
                activeButtonText = interact;
                break;
            case "bio":
                activeButtonText = bio;
                break;
        }
    }

    private void Update()
    {
        if (isListening)
        {
            Debug.Log("hmmmmm");
            string keyPressed = null;

            string[] qwerty =
            {
            "`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=",
            "q", "e", "r", "t", "y", "u", "i", "o", "p", "[", "]", "\\",
            "f", "g", "h", "j", "k", "l", ";", "'",
            "z", "x", "c", "v", "b", "n", "m", ",", ".", "/"
            };


            foreach (string charecter in qwerty)
            {
                if (Input.GetKeyDown(charecter) && charecter != inventory_Char && charecter != follower_Char && charecter != interact_Char && charecter != bio_Char)
                {
                    Debug.Log("Huzzah!! \n" + charecter + " is the chosen one!");
                    keyPressed = charecter;

                    switch (activeString)
                    {
                        case "inventory":
                            inventory_Char = charecter;
                            break;
                        case "follower":
                            follower_Char = charecter;
                            break;
                        case "interact":
                            interact_Char = charecter;
                            break;
                        case "bio":
                            bio_Char = charecter;
                            break;
                    }

                    activeButtonText.GetComponent<Text>().text = keyPressed.ToUpper();
                    isListening = false;

                    continue;
                }
            }
        }
    }
}
