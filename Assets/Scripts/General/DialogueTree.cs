using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//I thought this was supposed to do way more stuff but actually it's all in the dialogue controller

//not sure what monobehaviour is for but it got mad at me when it wasn't there
//also it said it should be an abstract classn in the web?
public abstract class DialogueTree : MonoBehaviour
{
    //make a string instead of the thingy 
    //so you can use the code on all npcs 
    //cause it's public its availbe in inspector
    //public string CSVFileName;
    //nvm pretty sure we don't need this
    public float RelationshipType;

    //I think this is supposed to be built off the csv
    public Statement[] conversationPoints;

    public TextAsset csv;

    //not sure where this would be in the csv or what it's like supposed to do
    //is it if your reputation hits zero? 
    public string GoAway;

    //maybe this is how to puit in csvInformation but seems complicated
    //I think that was unnecessary
        //public TextAsset csvInformation;
}