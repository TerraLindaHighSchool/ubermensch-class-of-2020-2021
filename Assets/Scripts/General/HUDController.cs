using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* HUD 0 IS CONVERSATION 
 * HUD 1 IS COMBAT
 * HUD 2 IS INVENTORY
 */
public class HUDController : MonoBehaviour
{
    private int activeHUD;
    public GameObject[] Huds;
    public bool inConvo = false;
    private GameObject activeNpc;

    //Dialogue HUD Fields

    public GameObject[] buttons;
    public GameObject npcName;
    public GameObject npcSpeak;

    //Disables the previously actived hud, activates the new hud, 
    //and sets the new hud to be the active hud
    public void HUDLoader(int hud, GameObject caller)
    {
        if(hud< Huds.Length)
        {
            Huds[activeHUD].SetActive(false);
            activeHUD = hud;
            Huds[activeHUD].SetActive(true);
        }
    }

    //Same as HUDLoader, but also sets the active Npc and starts a conversation if necessary
    public void HUDLoader(int hud, GameObject caller, GameObject Npc)
    {
        if (hud < Huds.Length)
        {
            Huds[activeHUD].SetActive(false);
            activeHUD = hud;
            Huds[activeHUD].SetActive(true);
            activeNpc = Npc;
            if (activeHUD == 0)
            {
                inConvo = true;
                convoLoader(activeNpc.GetComponent<DialogueController>().StartConversation());
            }
            Debug.Log("HUD Loaded");
        }
        else
        {
            Debug.Log("HUD could not load");
        }
    }

    //Called on button press to continue the conversation
    public void continueConvo(int buttonNumber)
    {
        if (inConvo)
        {
             convoLoader(activeNpc.GetComponent<DialogueController>().LoadNext(buttonNumber));
        }
        Debug.Log(buttonNumber + " pressed");
    }

    public void convoLoader(Statement info)
    {
        npcSpeak.GetComponent<Text>().text = info.NpcLine;
        buttons[0].GetComponent<Text>().text = info.Response[0];
        buttons[1].GetComponent<Text>().text = info.Response[1];
        buttons[2].GetComponent<Text>().text = info.Response[2];
        buttons[3].GetComponent<Text>().text = info.Response[3];
    }

    //Disables the active hud
    public void HUDDeLoader(int hud)
    {
        Huds[hud].SetActive(false);
        Debug.Log("HUD Unloaded");
    }
}
