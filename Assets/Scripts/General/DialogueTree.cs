using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//I thought this was supposed to do way more stuff but actually it's all in the dialogue controller

//not sure what monobehaviour is for but it got mad at me when it wasn't there
//also it said it should be an abstract classn in the web?
abstract class DialogueTree : MonoBehaviour
{
    //make a string instead of the thingy 
    //so you can use the code on all npcs 
    //cause it's public its availbe in inspector
    public string CSVFileName;

    //I think this is supposed to be built off the csv
    public Statement[] conversationPoints;

    //not sure but 
    public string GoAway;

    //maybe this is how to puit in csvInformation but seems complicated
    public TextAsset csvInformation;

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

    void Update()
    {

    }

}