using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private int activeHUD;
    public GameObject[] HUDs;
    public bool inConvo = false;
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

    public void HUDLoader(int hud, GameObject caller, GameObject Npc)
    {
        if (hud < HUDs.Length)
        {
            HUDs[activeHUD].SetActive(false);
            activeHUD = hud;
            HUDs[activeHUD].SetActive(true);
            if (activeHUD == 0)
            {
                /*
                Npc.StartConversation();
                inConvo = true;
                */
            }
        }
    }

    public void continueConvo(int buttonNumber, GameObject Npc)
    {
        if(inConvo)
        {
            /*
             * Npc.LoadNext(buttonNumber)
             */
        }
    }
    private void printDialogue(GameObject Npc)
    {

    }
    //Disables the active hud
    public void HUDDeLoader(int hud)
    {
        HUDs[hud].SetActive(false);
    }
}
