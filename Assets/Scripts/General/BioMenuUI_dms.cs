using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BioMenuUI_dms : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text Description;
    [SerializeField] Text Mission;
    void Start()
    {
        //This is placeholder text
        //It will grab text from NPC Matrix when it is made
        Description.text = "Character Description";
        Mission.text = "Character's Missions";
    }
    
}
