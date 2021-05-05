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
    public InventoryManager playerInventory;
    public StandardInventoryItem rock;
    public StandardInventoryItem empty;


    //Dialogue HUD Fields

    public GameObject[] dialogueButtons;
    public GameObject npcName;
    public GameObject npcSpeak;
    public bool inConversation = false;

    //Inventory HUD Fields

    public GameObject[] inventoryButtonsHUD;
    public bool invOpen = false;
    public List<InventoryItemInterface> inventory;
    public GameObject selectedItem;
    public GameObject[] selectedText;
    
    /*
     * HUD LOADER AND DELOADER
     */

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
                inventory = playerInventory.PrintInventory();
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

    public void HUDLoader()
    {
        if (activeHUD < Huds.Length)
        {
            Huds[activeHUD].SetActive(false);
            Huds[activeHUD].SetActive(true);
            if (activeHUD == 0)
            {
                Debug.Log("ERROR: Please use HUDLoader(int hud, GameObject caller, GameObject Npc)");
            }
            if (activeHUD == 1)
            {
                Debug.Log("Inventory");
                invOpen = true;
                inventory = playerInventory.PrintInventory();
                inventoryLoader(inventory, 1);
            }
            Debug.Log("HUD Loaded");
        }
        else
        {
            Debug.Log("HUD could not load");
        }
    }

    //Disables the active hud
    public void HUDDeLoader(int hud)
    {
        if (hud == 0)
        {
            inConversation = false;
            Debug.Log("Dialogue");
        }
        if (hud == 1)
        {
            invOpen = false;
            Debug.Log("Inventory");
        }
        Huds[hud].SetActive(false);
        Debug.Log("HUD Unloaded");
    }

    /*
     *  INVENTORY HUD METHODS
     */

    public void inventoryLoader(List<InventoryItemInterface> inventory, int hud)
    {
        int hudSpace = 0;
        List<GameObject> localInv = new List<GameObject>();
        
        switch (hud)
        {
            case 1:
                hudSpace = 24;
                foreach (GameObject obj in inventoryButtonsHUD)
                {
                    localInv.Add(obj);
                }
                break; 
        }
        if(inventory.Count > localInv.Count)
        {
            Debug.Log("count too small");
            return;
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log("i = " + i);
            localInv[i].GetComponent<Image>().sprite = inventory[i].Icon;
            if (localInv[i].GetComponent<InventoryContainer>() != null)
            {
                Destroy(localInv[i].GetComponent<InventoryContainer>());
            }
            localInv[i].AddComponent<InventoryContainer>();
            localInv[i].GetComponent<InventoryContainer>().item = inventory[i];
            localInv[i].GetComponent<Image>().sprite = inventory[i].Icon;
        }
    }

    public void itemClick(int buttonNumber)
    {
        if(inventory.Count >= buttonNumber)
        {
            selectedItem.GetComponent<Image>().sprite = inventory[buttonNumber].Icon;
            selectedText[0].GetComponent<Text>().text = inventory[buttonNumber].Name;
            selectedText[1].GetComponent<Text>().text = inventory[buttonNumber].Value;
            selectedText[2].GetComponent<Text>().text = inventory[buttonNumber].ToolTip;
            Debug.Log(buttonNumber + " was selected");
        }
        else
        {
            Debug.Log("oopsie no item");
        }
    }

    public void inventoryPreloader(List<InventoryItemInterface> inventory, int hud)
    {
        for(int i = 1; i < 25; i++)
        {
            this.GetComponentInParent<InventoryManager>().AddItem(empty);


        }
    }

    /*
     * DIALOGUE HUD METHODS
     */

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
        dialogueButtons[0].GetComponentInChildren<Text>().text = info.Response[0];
        dialogueButtons[1].GetComponentInChildren<Text>().text = info.Response[1];
        dialogueButtons[2].GetComponentInChildren<Text>().text = info.Response[2];
        dialogueButtons[3].GetComponentInChildren<Text>().text = info.Response[3];
    }    
}
