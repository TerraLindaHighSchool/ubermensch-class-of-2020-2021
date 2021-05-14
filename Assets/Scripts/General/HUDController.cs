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
                        // FIELDS

    //GENERAL HUD FIELDS
    private int activeHUD;
    public GameObject[] Huds;
    private GameObject activeNpc;
    public GameObject player;

    //GENRAL INVENTORY FIELDS
    public StandardInventoryItem empty; // This is used to DEFAULT empty slots
    public bool invOpen = false; // If an inventory is loaded
    public GameObject[] inventoryButtonsHUD; // These are the BUTTONS on the INVENTORY menu
    public GameObject[] equipButtonsHUD; // These are the BUTTONS on the EQUIP menu
    private List<InventoryItemInterface> _inventoryAuxillary; // This is 
    public List<InventoryItemInterface> inventoryAuxillary
    {
        get { return _inventoryAuxillary; }
        set
        {
            Debug.Log("InvAux set" + gameObject.GetInstanceID());
            _inventoryAuxillary = value;
        }
    }

    //PLAYER FIELDS;
    public InventoryManager main; // This is the PLAYERS main inventory
    public InventoryManager equipMenu; // This is the PLAYERS equip
    
    //TRADE FIELDS
    public GameObject selectedItem; // This is the information of the curently selected item
    public GameObject[] selectedText; // These are the TEXT GameObjects assigned in the INSPECTOR that display item information
    public int selectedNumber; // This is the Number of the currently selected Item

    //DIALOUGE HUD FIELDS
    public GameObject[] dialogueButtons;
    public GameObject npcName;
    public GameObject npcSpeak;
    public bool inConversation = false;

    // HUD LOADER AND DELOADER

    private void Awake()
    {
        Debug.Log("I the HUD Manager, Exist!");
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
                determineInv();
                inventoryLoader(equipMenu.PrintInventory(), 2);
                inventoryLoader(main.PrintInventory(), 1);
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
                conversationLoader(activeNpc.GetComponentInChildren<DialogueController>().StartConversation());
            }
            Debug.Log("HUD Loaded");
        }
        else
        {
            Debug.Log("HUD could not load");
        }
    }

    //This is used to reload the inventory
    public void HUDLoader()
    {
        Debug.Log("HUD re-loaded?");

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
                determineInv();
                inventoryLoader(main.PrintInventory(), 1);
                inventoryLoader(equipMenu.PrintInventory(), 2);
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
            selectedItem.GetComponent<Image>().sprite = empty.Icon;
            selectedText[0].GetComponent<Text>().text = ("");
            selectedText[1].GetComponent<Text>().text = ("");
            selectedText[2].GetComponent<Text>().text = ("No Item is Selected");
            selectedText[3].SetActive(false);
            Debug.Log("Inventory");
        }
        Huds[hud].SetActive(false);
        Debug.Log("HUD Unloaded");
    }

    /*
     *  INVENTORY HUD METHODS
     */

    //Loads every single different inventory.
    //Currently only loads the player inventory, as other inventories do not exist yet
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
            case 2:
                hudSpace = 4;
                inventoryAuxillary = inventory;
                foreach (GameObject obj in equipButtonsHUD)
                {
                    localInv.Add(obj);
                }
                break;
        }
        if (inventory.Count > localInv.Count)
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
        for (int i = inventory.Count; i < localInv.Count; i++)
        {
            localInv[i].GetComponent<Image>().sprite = empty.Icon;
        }
        selectedText[3].SetActive(false);
    }

    //When an inventory item is clicked, it shows the proper icon, name, value, and description
    public void itemClick(int buttonNumber)
    {
        selectedNumber = buttonNumber;
        List<InventoryItemInterface> inventoryClicked;

        if (buttonNumber > 23)
        {
            selectedText[3].GetComponentInChildren<Text>().text = ("Unequip");
        }
        else
        {
            selectedText[3].GetComponentInChildren<Text>().text = ("Equip");
        }

        if (buttonNumber <= 23)
        {
            inventoryClicked = main.PrintInventory();
        }
        else
        {
            inventoryClicked = inventoryAuxillary;
            buttonNumber -= 24;
        }

        if (inventoryClicked.Count > buttonNumber)
        {
            selectedItem.GetComponent<Image>().sprite = inventoryClicked[buttonNumber].Icon;
            selectedText[0].GetComponent<Text>().text = inventoryClicked[buttonNumber].Name;
            selectedText[1].GetComponent<Text>().text = inventoryClicked[buttonNumber].Value.ToString();
            selectedText[2].GetComponent<Text>().text = inventoryClicked[buttonNumber].ToolTip;
            selectedText[3].SetActive(true);
            Debug.Log(buttonNumber + " was selected");
        }
        else
        {
            selectedItem.GetComponent<Image>().sprite = empty.Icon;
            selectedText[0].GetComponent<Text>().text = empty.Name;
            selectedText[1].GetComponent<Text>().text = ("");
            selectedText[2].GetComponent<Text>().text = empty.ToolTip;
            selectedText[3].SetActive(false);
            Debug.Log(buttonNumber + " was selected, but is empty :(");
        }
    }

    public void determineInv()
    {
        InventoryManager[] menus = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<InventoryManager>();
        if (menus[0].inventoryType == InventoryManager.InvType.EquipMenu)
        {
            equipMenu = menus[0];
            main = menus[1];
        }
        else
        {
            equipMenu = menus[1];
            main = menus[0];
        }
    }
    //Equips an item when the equip button is clicked
    public void itemEquip()
    {
        if(selectedNumber <= 23)
        {
            Debug.Log("Equip HUD Instance ID " + gameObject.GetInstanceID());
            GetComponent<ItemTransferManager>().Transfer(main, equipMenu, main.PrintInventory()[selectedNumber]);
        }
        else
        {
            GetComponent<ItemTransferManager>().Transfer(equipMenu, main, inventoryAuxillary[selectedNumber - 24]);
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
        npcName.GetComponent<Text>().text = activeNpc.GetComponent<Follower>().identity.GetDisplayName();
        dialogueButtons[0].GetComponentInChildren<Text>().text = info.Response[0];
        dialogueButtons[1].GetComponentInChildren<Text>().text = info.Response[1];
        dialogueButtons[2].GetComponentInChildren<Text>().text = info.Response[2];
        dialogueButtons[3].GetComponentInChildren<Text>().text = info.Response[3];
    }    
}
