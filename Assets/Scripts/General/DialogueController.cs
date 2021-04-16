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
    private bool inCombat; // if not in combat in conversation

    //this should probably go here or else it maybe could go into onenable
    //dont know what to name these
    //you just drag the csv file in the inspector as long as like it follows the rules and stuff


    //find dialoguetree and combat tree and assign them to conversation and combat respectiveley respectively
    //by find I think it will just get that from the textassets that you put in the inspector
    //isn't there supposed to be like a public string thingy?
    public void OnEnable()
    {
        //uses csvreader for combat and conversation
        //it maybe looks like this
        conversation.conversationPoints = csvReader(conversation.csv);
        combat.conversationPoints = csvReader(conversation.csv);
    }

    //for start conversation tree? (load the first item in the start conversation tree)
    public Statement StartConversation()
    {
        //not sure what it means by "load"
        //I guess it means return but then it should return not load
        //this would use dialogue tree conversation

        inCombat = false;
        return conversation.conversationPoints[0];
    }

    public Statement StartCombat()
    {
        inCombat = true;
        return combat.conversationPoints[0];
    }

    public Statement LoadNext(int Option)
    {
        DialogueTree activeDialogueTree;

        if (inCombat) { activeDialogueTree = combat; }
        else { activeDialogueTree = conversation; }

        setRelationshipType(activeDialogueTree.conversationPoints[currentposition].ResponseModifier[Option - 1]);

        currentposition = activeDialogueTree.conversationPoints[currentposition].ResponseOutcome[Option - 1];

        return activeDialogueTree.conversationPoints[currentposition];
    }

    public string Closer()
    {
        return conversation.GoAway;
    }

    //modifies relationship type
    private void setRelationshipType(float change)
    {
        conversation.RelationshipType += change; //Relationship Type? 
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
        string[] row = data[0].Split(new char[] { '\t' });

        Statement[] ParsedCsv = new Statement[(data.Length - 2)];
        //for my csv file it should start on 2? (the row with what we want was on 3 but since it's an array it would be one less)
        //data maybe should be something else,
        //no minus 1 because my csv didn't have a line on the last
        for (int i = 2; i < data.Length; i++)
        {
            //idk about calling it row or not
            //get a split thing that does tabbs now and stuff?

            row = data[i].Split(new char[] { '\t' });
            //how many rows of "data" there are?
            //this creates the statement array in the dialogue tree thing for how long it should be

            //something like "for row.length"

            //assuming the rows follow the format from the csv this was written for
            //(row.Length-2?)/3
            //this would be the number/size for the arrays for the options



            //npc line 
            string npcLine = row[2];

            //we need to find out howmany is the opions hence -2 (row numer and npc line) div three for how many
            string[] response = new string[(row.Length - 1) / 3];
            float[] responseModifier = new float[(row.Length - 1) / 3];
            int[] responseOutcome = new int[(row.Length - 1) / 3];

            for (int j = 1; j < row.Length - 1; j += 3)
            {
                //the third in row array because thats how arrays work
                response[j] = row[j + 2];
                float Fstore = 0;
                if (float.TryParse(row[j + 3], out Fstore))
                {
                    responseModifier[j] = Fstore;
                }
                int Istore = 0;
                if (int.TryParse(row[j + 4], out Istore))
                {
                    responseOutcome[j] = Istore;
                }
            }

            //this is i-2 would be 0 since arrays start at zero
            ParsedCsv[i - 2] = new Statement(npcLine, response, responseModifier, responseOutcome);

            //there needs to be prebuilt the variables/arrays since they have to go in the constructor

            //statement construcotr
            //public Statement(string NpcL, string[] R, float[] RM, int[] RO)
            //{
            //    NpcLine = NpcL;
            //    Response = R;
            //    ResponseModifier = RM;
            //    ResponseOutcome = RO;
            //}


            //this is a statement array in the dialogue tree
            //csvInfo.conversationPoints
        }

        return ParsedCsv;
    }
}