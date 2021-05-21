using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransferManager : MonoBehaviour
{
    public bool Transfer(InventoryManager sender, InventoryManager reciever, InventoryItemInterface item)
    {
        bool successful;
        if (sender.inventoryType == InventoryManager.InvType.Player && reciever.inventoryType == InventoryManager.InvType.Player)
        {
            if(reciever.soap >= item.Value && reciever.AddItem(item))
            {
                sender.RemoveItem(item);
                sender.soap += item.Value;
                reciever.soap -= item.Value;
                successful = true;
            }
            else
            {
                Debug.Log("Trade Failed");
                successful = false;
            }
        }
        else
        {
            if(reciever.AddItem(item))
            {
                sender.RemoveItem(item);
                if(reciever.inventoryType == InventoryManager.InvType.EquipMenu)
                {
                    PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                    player.playerStrength += item.StrengthBoost;
                    player.playerCharisma += item.CharismaBoost;
                    player.playerConstitution += item.ConstitutionBoost;
                }
                if (reciever.inventoryType == InventoryManager.InvType.Player)
                {
                    PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                    player.playerStrength -= item.StrengthBoost;
                    player.playerCharisma -= item.CharismaBoost;
                    player.playerConstitution -= item.ConstitutionBoost;
                }
                successful = true;
            }
            else
            {
                Debug.Log("Equip Failed");
                successful = false;
            }
        }
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDController>().HUDLoader();
        return successful;
    }
}
