﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//I thought this was supposed to do way more stuff but actually it's all in the dialogue controller

//not sure what monobehaviour is for but it got mad at me when it wasn't there
//also it said it should be an abstract classn in the web?
public abstract class DialogueTree : MonoBehaviour
{
    //its not capital r so it may mess stuff up now
    public float relationshipType;

    //I think this is supposed to be built off the csv
    public Statement[] conversationPoints;

    public TextAsset csv;

    //not sure where this would be in the csv or what it's like supposed to do
    //is it if your reputation hits zero? 
    public string GoAway;

    //maybe this is how to puit in csvInformation but seems complicated
    //I think that was unnecessary
        //public TextAsset csvInformation;

    void Start()
    {
        /*
        //hopefully this is right //its not
        TextAsset csvInformation = Resources.Load<TextAsset>(CSVFileName);
        
        // \n is the thingy for new line
        string[] data = csvInformation.text.Split(new char[] { '\n' });

        //this is assuming there is the collumn labelly things in the first row and nothing in the very last row
        for (int i = 0; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

        }
        */
    }
}