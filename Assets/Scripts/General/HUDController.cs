﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* HUD 0 IS CONVERSATION 
 * HUD 1 IS INVENTORY
 * HUD 2 IS TRADE INVENTORY
 * HUD 3 IS PLAYER FOLLOWER MENU
 * HUD 4 IS HOMEBASE FOLLOWER MENU
 * HUD 5 IS ACTIVE HUD
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
    public GameObject[] npcTradeButtonsHUD; // These are the BUTTONS on the npc TRADE menu
    public GameObject[] playerTradeButtonsHUD; // These are the BUTTONS on the player TRADE menu
    public InventoryManager npcInv; //This is the NPC's Inventory
    public GameObject npcTradeName;
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
    public GameObject[] tradeSelectedItem; // These are the icons during a trade, 0 is the player's item, 1 is the npc's item
    public GameObject[] tradeSelectedText; // These are the same as the selected text array, with 0-3 being for the player and 4-7 being for the npc
    public GameObject[] selectedText; // These are the TEXT GameObjects assigned in the INSPECTOR that display item information (0 is name 1 is value 2 is tooltip 3 is equip)
    public int selectedNumber; // This is the Number of the currently selected Item
    public GameObject playerSoap;

    //DIALOUGE HUD FIELDS
    public GameObject[] dialogueButtons;
    public GameObject npcName;
    public GameObject npcSpeak;
    public bool inConversation = false;

    //FOLLOWER HUD FIELDS
    public GameObject[] followerNames = new GameObject[20];
    public GameObject[] followerDescriptions = new GameObject[20];
    public GameObject[] followerIcons = new GameObject[20];
    public GameObject[] followerButtons = new GameObject[20];
    public bool followerMenuIsOpen = false;
    public FollowerTrader FollowerMover;

    //ACTIVE HUD FIELDS
    public GameObject hudFood;
    public GameObject hudOxygen;
    public GameObject hudSoap;
    public GameObject hudHealth;

    // HUD LOADER AND DELOADER

    private void Awake()
    {
        determineInv();
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
            else if(activeHUD == 1)
            {
                Debug.Log("Inventory");
                invOpen = true;
                determineInv();
                inventoryLoader(equipMenu.PrintInventory(), 2);
                inventoryLoader(main.PrintInventory(), 1);
            }
            else if(activeHUD == 2)
            {
                Debug.Log("ERROR: Please use HUDLoader(int hud, GameObject caller, GameObject Npc)");
            }
            else if(activeHUD == 3)
            {
                updateFollowers(true);
                Debug.Log("Player Follower");
            }
            else if(activeHUD == 4)
            {
                updateFollowers(false);
                Debug.Log("HomeBase Follower");
            }
            else if(activeHUD == 5)
            {
                loadBasicPlayerInfo();
                Debug.Log("Active");
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
            activeNpc = Npc;
            if (activeHUD == 0)
            {
                Debug.Log("Dialogue");
                inConversation = true;
                conversationLoader(activeNpc.GetComponent<DialogueController>().StartConversation());
            }
            if(activeHUD == 2)
            {
                npcInv = activeNpc.GetComponent<InventoryManager>();
                Debug.Log(activeNpc.GetComponent<Follower>().identity.GetDisplayName());
                npcTradeName.GetComponent<Text>().text = activeNpc.GetComponent<Follower>().identity.GetDisplayName();
                Debug.Log("Inventory");
                invOpen = true;
                determineInv();
                inventoryLoader(main.PrintInventory(), 3);
                inventoryLoader(npcInv.PrintInventory(), 4);
                playerSoap.GetComponent<Text>().text = main.soap.ToString();
            }
            if(activeHUD == 3)
            {
                Debug.Log("ERROR: Please use HUDLoader(int hud, GameObject caller)");
            }
            if (activeHUD == 4)
            {
                Debug.Log("ERROR: Please use HUDLoader(int hud, GameObject caller)");
            }
            if (activeHUD == 5)
            {
                Debug.Log("ERROR: Please use HUDLoader(int hud, GameObject caller)");
            }
            Debug.Log("HUD Loaded");
        }
        else
        {
            Debug.Log("HUD could not load");
        }
        Huds[activeHUD].SetActive(true);
    }

    //This is used to reload the inventory
    public void HUDLoader()
    {
        Debug.Log("HUD re-loaded?");
        Debug.Log("active hud is number " + activeHUD);
        if (activeHUD < Huds.Length)
        {
            Huds[activeHUD].SetActive(false);
            Huds[activeHUD].SetActive(true);
            if (activeHUD == 0)
            {
                Debug.Log("ERROR: Please use HUDLoader(int hud, GameObject caller, GameObject Npc)");
            }
            else if (activeHUD == 1)
            {
                Debug.Log("Inventory");
                invOpen = true;
                determineInv();
                inventoryLoader(main.PrintInventory(), 1);
                inventoryLoader(equipMenu.PrintInventory(), 2);
            }
            else if(activeHUD == 2)
            {
                Debug.Log("Trade");
                invOpen = true;
                determineInv();
                inventoryLoader(main.PrintInventory(), 3);
                inventoryLoader(npcInv.PrintInventory(), 4);
                playerSoap.GetComponent<Text>().text = main.soap.ToString();
            }
            else if(activeHUD == 3)
            {
                updateFollowers(true);
                Debug.Log("Player Follower");
            }
            else if(activeHUD == 4)
            {
                updateFollowers(false);
                Debug.Log("HomeBase Follower");
            }
            else if (activeHUD == 5)
            {
                loadBasicPlayerInfo();
                Debug.Log("Active");
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
        Huds[hud].SetActive(false);

        if (hud == 0)
        {
            inConversation = false;
            Debug.Log("Dialogue");
            if (activeNpc.GetComponent<DialogueController>().AskedToJoin())
            {
                activeNpc.GetComponent<DialogueController>().RecruitmentCheck();
            }
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
        if(hud == 2)
        {
            Debug.Log("Trade Menu");
        }
        if(hud == 3)
        {
            Debug.Log("Player Follower");
        }
        if(hud == 4)
        {
            Debug.Log("HomeBase Follower");
        }
        Debug.Log("HUD Unloaded");
    }

    /*
     *  INVENTORY HUD METHODS
     */

    //Loads every single different inventory.
    //Currently loads the player inventory with 1, the equip inventory with 2, the player trade inventory with 3, and the NPC trade inventory with 4
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
            case 3:
                hudSpace = 24;
                foreach (GameObject obj in playerTradeButtonsHUD)
                {
                    localInv.Add(obj);
                }
                break;
            case 4:
                hudSpace = 24;
                inventoryAuxillary = inventory;
                foreach (GameObject obj in npcTradeButtonsHUD)
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
        tradeSelectedText[3].SetActive(false);
        tradeSelectedText[7].SetActive(false);
    }

    //When an inventory item is clicked, it shows the proper icon, name, value, and description
    public void itemClick(int buttonNumber)
    {
        Debug.Log("active HUD is number " + activeHUD);
        selectedNumber = buttonNumber;
        List<InventoryItemInterface> inventoryClicked;
        int npcTradeBoost = 0;
        determineInv();
        if (buttonNumber > 23)
        {
            if(activeHUD == 1)
            {
                selectedText[3].GetComponentInChildren<Text>().text = ("Unequip");
            }
            else if(activeHUD == 2)
            {
                tradeSelectedText[7].GetComponentInChildren<Text>().text = ("Buy");
            }
        }
        else
        {
            if (activeHUD == 1)
            {
                selectedText[3].GetComponentInChildren<Text>().text = ("Equip");
            }
            else if(activeHUD == 2)
            {
                tradeSelectedText[3].GetComponentInChildren<Text>().text = ("Sell");
            }
        }
        if (buttonNumber <= 23)
        {
            inventoryClicked = main.PrintInventory();
        }
        else
        {
            inventoryClicked = inventoryAuxillary;
            buttonNumber -= 24;
            npcTradeBoost = 1;
        }
        if (inventoryClicked.Count > buttonNumber)
        {
            if (activeHUD == 1)
            {
                selectedItem.GetComponent<Image>().sprite = inventoryClicked[buttonNumber].Icon;
                selectedText[0].GetComponent<Text>().text = inventoryClicked[buttonNumber].Name;
                selectedText[1].GetComponent<Text>().text = inventoryClicked[buttonNumber].Value.ToString();
                selectedText[2].GetComponent<Text>().text = inventoryClicked[buttonNumber].ToolTip;
                selectedText[3].SetActive(true);
            }
            else if(activeHUD == 2)
            {
                tradeSelectedItem[0 + npcTradeBoost].GetComponent<Image>().sprite = inventoryClicked[buttonNumber].Icon;
                tradeSelectedText[0 + 4*npcTradeBoost].GetComponent<Text>().text = inventoryClicked[buttonNumber].Name;
                tradeSelectedText[1 + 4 * npcTradeBoost].GetComponent<Text>().text = inventoryClicked[buttonNumber].Value.ToString();
                tradeSelectedText[2 + 4 * npcTradeBoost].GetComponent<Text>().text = inventoryClicked[buttonNumber].ToolTip;
                tradeSelectedText[3 + 4 * npcTradeBoost].SetActive(true);
            }

            Debug.Log(buttonNumber + " was selected");
        }
        else
        {
            if (activeHUD == 1)
            {
                selectedItem.GetComponent<Image>().sprite = empty.Icon;
                selectedText[0].GetComponent<Text>().text = empty.Name;
                selectedText[1].GetComponent<Text>().text = ("");
                selectedText[2].GetComponent<Text>().text = empty.ToolTip;
                selectedText[3].SetActive(false);
            }
            else if(activeHUD == 2)
            {
                tradeSelectedItem[0].GetComponent<Image>().sprite = empty.Icon;
                tradeSelectedItem[1].GetComponent<Image>().sprite = empty.Icon;
                tradeSelectedText[0].GetComponent<Text>().text = empty.Name;
                tradeSelectedText[1].GetComponent<Text>().text = ("");
                tradeSelectedText[2].GetComponent<Text>().text = empty.ToolTip;
                tradeSelectedText[3].SetActive(false);
                tradeSelectedText[4].GetComponent<Text>().text = empty.Name;
                tradeSelectedText[5].GetComponent<Text>().text = ("");
                tradeSelectedText[6].GetComponent<Text>().text = empty.ToolTip;
                tradeSelectedText[7].SetActive(false);
            }
            Debug.Log(buttonNumber + " was selected, but is empty :(");
        }
    }

    //Sets equipMenu and main to the proper InventoryManagers of the Player
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

    //Trades the selected item when the button is pressed
    public void itemTrade()
    {
        Debug.Log("traded :)");
        if (selectedNumber <= 23)
        {
            Debug.Log("Trade HUD Instance ID " + gameObject.GetInstanceID());
            GetComponent<ItemTransferManager>().Transfer(main, npcInv, main.PrintInventory()[selectedNumber]);
        }
        else
        {
            GetComponent<ItemTransferManager>().Transfer(npcInv, main, inventoryAuxillary[selectedNumber - 24]);
        }
        playerSoap.GetComponent<Text>().text = main.soap.ToString();
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

    /*
     * FOLLOWER HUD METHODS
     */

    //Based on if the Player follower list or Homebase follower list is being used, the proper follower list is loaded onto the HUD
    public void updateFollowers(bool isInPlayer) 
    {
        FollowerManager currentManager;
        FollowerIdentity[] currentFollowers;
        if (!isInPlayer)
        {
            Debug.Log("HomeBase Follower Manager is Being Used");
            currentManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FollowerManager>();
            currentFollowers = currentManager.PrintFollowers();
        }
        else
        {
            Debug.Log("Player Follower Manager is Being Used");
            currentManager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FollowerManager>();
            currentFollowers = currentManager.PrintFollowers();
        }
        for (int i = 0; i < currentFollowers.Length; i++)
        {
            followerDescriptions[i].GetComponentInChildren<Text>().text = currentFollowers[i].GetDisplayDescription();
            followerNames[i].GetComponentInChildren<Text>().text = currentFollowers[i].GetDisplayName();
            followerIcons[i].GetComponentInChildren<Image>().sprite = currentFollowers[i].Icon;
            if(!isInPlayer)
            {
                followerButtons[i].GetComponentInChildren<Text>().text = ("Recruit");
            }
            else
            {
                followerButtons[i].GetComponentInChildren<Text>().text = ("Send Home");
            }
            followerDescriptions[i].SetActive(true);
            followerNames[i].SetActive(true);
            followerIcons[i].SetActive(true);
            followerButtons[i].SetActive(true);
            Debug.Log(currentFollowers[i].GetDisplayName());
        }
        for (int i = currentFollowers.Length; i < 20; i++)
        {
            deactivateFollower(i);
        }
        if (currentFollowers.Length < 7)
        {
            GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>().size = 0.99f;
        }
        else
        {
            GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>().size = (1.2943f - .0491f * currentFollowers.Length);
        }
    }

    //Kills eye
    private void deactivateFollower(int i)
    {
        followerDescriptions[i].SetActive(false);
        followerNames[i].SetActive(false);
        followerIcons[i].SetActive(false);
        followerButtons[i].SetActive(false);
    }
    //Moves the followers between the two follower lists on button press
    public void moveFollowers(int buttonNumber)
    {
        if (activeHUD == 3)
        {
            FollowerMover.MoveFollower(buttonNumber, true);
        }
        else if (activeHUD == 4)
        {
            FollowerMover.MoveFollower(buttonNumber, false);
        }
        Debug.Log(followerNames[buttonNumber].GetComponentInChildren<Text>().text + " is no longer with us :)");
        deactivateFollower(buttonNumber);
        HUDLoader();
    }

    /*
     * ACTIVE HUD METHODS
     */

    public void loadBasicPlayerInfo()
    {
        PlayerController playerHUDDetails = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
        determineInv();
        hudSoap.GetComponentInChildren<Text>().text = main.soap.ToString();
        hudFood.GetComponentInChildren<Text>().text = playerHUDDetails.food.ToString();
        hudHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(13, (playerHUDDetails.health / 100 * 50.1f) + 2.9f);
        hudOxygen.GetComponent<RectTransform>().sizeDelta = new Vector2(18, (playerHUDDetails.oxygen / 100 * 33.95f) + 3.1f);
    }
}
