using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* HUD 0 IS CONVERSATION 
 * HUD 1 IS INVENTORY
 * HUD 2 IS EQUIP INVENTORY
 */
public class HUDController : MonoBehaviour
{
    private int activeHUD;
    public GameObject[] Huds;
    private GameObject activeNpc;
    public GameObject player;

    //Dialogue HUD Fields

    public GameObject[] dialogueButtons;
    public GameObject npcName;
    public GameObject npcSpeak;
    public bool inConversation = false;

    //Inventory HUD Fields

    public GameObject[] inventoryButtons;
    public bool invOpen = false;
    public List<InventoryItemInterface> inventory;
    

    //Disables the previously actived hud, activates the new hud, 
    //and sets the new hud to be the active hud
    public void HUDLoader(int hud, GameObject caller)
    {
        if (hud < Huds.Length)
        {
            Huds[activeHUD].SetActive(false);
            activeHUD = hud;
            Huds[activeHUD].SetActive(true);
            if(activeHUD == 0)
            {
                Debug.Log("ERROR: Please use HUDLoader(int hud, GameObject caller, GameObject Npc)");
            }
            if (activeHUD == 1)
            {
                Debug.Log("Inventory");
                invOpen = true;
                inventory = player.GetComponent<InventoryManager>().PrintInventory();
                inventoryLoader(inventory, 1);
            }
            Debug.Log("HUD Loaded");
        }
        else
        {
            Debug.Log("HUD could not load");
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
                Debug.Log("Dialogue");
                inConversation = true;
                conversationLoader(activeNpc.GetComponent<DialogueController>().StartConversation());
            }
            Debug.Log("HUD Loaded");
        }
        else
        {
            Debug.Log("HUD could not load");
        }
    }

    public void inventoryLoader(List<InventoryItemInterface> inventory, int hud)
    {
        int hudSpace = 0;
        List<GameObject> localInv = new List<GameObject>();
        
        switch (hud)
        {
            case 1:
                hudSpace = 24;
                foreach (GameObject obj in inventoryButtons)
                {
                    localInv.Add(obj);
                }
                break; 
        }
        for (int i = 0; i < hudSpace; i++)
        {
            localInv[i].GetComponent<Image>().sprite = inventory[i].Icon;
            if (inventoryButtons[i].GetComponent<Image>().sprite != null)
            {
                Destroy(inventoryButtons[i].GetComponent<Image>().sprite);
            }
            inventoryButtons[i].GetComponent<Image>().sprite = localInv[i].GetComponent<Image>().sprite;
        }
    }

    //Called on button press to continue the conversation
    public void continueConversation(int buttonNumber)
    {
        Debug.Log("Button " + buttonNumber + " was pressed :)");
        if(inConversation)
        {
             conversationLoader(activeNpc.GetComponent<DialogueController>().LoadNext(buttonNumber));
        }
    }

    //Loads a response based on which button was pressed
    public void conversationLoader(Statement info)
    {
        npcSpeak.GetComponent<Text>().text = info.NpcLine;
        dialogueButtons[0].GetComponent<Text>().text = info.Response[0];
        dialogueButtons[1].GetComponent<Text>().text = info.Response[1];
        dialogueButtons[2].GetComponent<Text>().text = info.Response[2];
        dialogueButtons[3].GetComponent<Text>().text = info.Response[3];
    }

    //Disables the active hud
    public void HUDDeLoader(int hud)
    {
        if(hud == 0)
        {
            inConversation = false;
        }
        if (hud == 1)
        {
            invOpen = false;
        }
        Huds[hud].SetActive(false);
        Debug.Log("HUD Unloaded");
    }
}
