﻿using System.Collections;
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
    public GameObject[] HUDs;
    public bool inConvo = false;
    public GameObject activeNpc;

    //Dialogue HUD Fields

    public GameObject[] buttons;
    public GameObject npcName;
    public GameObject npcSpeak;

    //Disables the previously actived hud, activates the new hud, 
    //and sets the new hud to be the active hud
    public void HUDLoader(int hud, GameObject caller)
    {
        if(hud< HUDs.Length)
        {
            HUDs[activeHUD].SetActive(false);
            activeHUD = hud;
            HUDs[activeHUD].SetActive(true);
        }
    }

    //Same as HUDLoader, but also sets the active Npc and starts a conversation if necessary
    public void HUDLoader(int hud, GameObject caller, GameObject Npc)
    {
        if (hud < HUDs.Length)
        {
            HUDs[activeHUD].SetActive(false);
            activeHUD = hud;
            HUDs[activeHUD].SetActive(true);
            activeNpc = Npc;
            if (activeHUD == 0)
            {
                inConvo = true;
                convoLoader(activeNpc.GetComponent<DialogueController>().StartConversation());
            }
        }
    }

    //Called on button press to continue the conversation
    public void continueConvo(int buttonNumber)
    {
        if(inConvo)
        {
             convoLoader(activeNpc.GetComponent<DialogueController>().LoadNext(buttonNumber));
        }
    }

    public void convoLoader(Statement info)
    {
        npcSpeak.GetComponent<Text>().text = info.NpcLine;
    }

    //Disables the active hud
    public void HUDDeLoader(int hud)
    {
        HUDs[hud].SetActive(false);
    }
}
