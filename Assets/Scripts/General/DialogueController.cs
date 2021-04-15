using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    //I think Dialogue_GruceBustin row 6 should probably not link to 6 or you get stuck in a loop lol
    //why did it move downa  row?
    //where does relationship start at
    //the statement only has one option but how many options are there supposed to be

    //dialogue tree would set how many options? as many as possible
    //

    private DialogueTree conversation;
    private DialogueTree combat;
    //not sure what this does
    private int currentposition;

    //this should probably go here or else it maybe could go into onenable
    //dont know what to name these
    //you just drag the csv file in the inspector as long as like it follows the rules and stuff
    public TextAsset combatcsv;
    public TextAsset conversationcsv;

    //find dialoguetree and combat tree and assign them to conversation and combat respectiveley respectively
    //by find I think it will just get that from the textassets that you put in the inspector
    //isn't there supposed to be like a public string thingy?
    public void OnEnable()
    {
        //uses csvreader for combat and conversation
        //it maybe looks like this
        conversation.conversationPoints = csvReader(conversationcsv);
        combat.conversationPoints = csvReader(combatcsv);
    }

    //for start conversation tree? (load the first item in the start conversation tree)
    public Statement StartConversation()
    {
        //not sure what it means by "load"
        //I guess it means return but then it should return not load
        //this would use dialogue tree conversation
        
        //return
    }

    public Statement StartCombat()
    {
        //this would use dialogue tree combat
       
        //return
    }

    public Statement LoadNext(int Option)
    {
        //I think it's supposed to keep track of the options?
        //int option would be the line of the csv 
        //wait how does it like get the option

        //return
    }

    //modifies relationship type
    private void setRelationshipType(float change)
    {

    }

    //csvinformation? statement is return type
    //I think this is where the magic happens
    //arrays start from zero
    
    private Statement[] csvReader(TextAsset csvInfo)
    {
        //bla bla bla parses csv into statement
        //returns statement array

        //goals for like getting it to work
        //1. it needs to like read one line from the csv to put it into a statement object thing
        //2. it needs to like account for the multiple rows for all the different options
        //3. it needs to put the options into an array for like the constructor
        //4. it needs to do that for all the "rows/options" in the csv

        //seems right
        string[] data = csvInfo.text.Split(new char[] { '\n' });

        //for my csv file it should start on 2? (the row with what we want was on 3 but since it's an array it would be one less)
        //data maybe should be something else,
        //no minus 1 because my csv didn't have a line on the last
        for (int i = 2; i < data.Length; i++)
        {



            //this is a statement array in the dialogue tree
            //csvInfo.conversationPoints
        }
        

    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
