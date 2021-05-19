﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
   
    private DialogueTree conversation;
    private int currentposition;
    private bool inCombat;
    private bool askedToJoin;

    private void OnEnable()
    {
        //uses csvreader for combat and conversation
        //it maybe looks like this
        conversation = this.gameObject.GetComponent<DialogueTree>();
        conversation.conversationPoints = csvReader(conversation.csv);
        Debug.Log("DialogueController Reader enabled for " + gameObject.name);
        //conversation.conversationPoints = csvReader(conversation.csv);
        //combat.conversationPoints = csvReader(conversation.csv);
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
        //not sure if this would work for option 0?
        setRelationshipType(activeDialogueTree.conversationPoints[currentposition].ResponseModifier[Option - 1]);

        currentposition = activeDialogueTree.conversationPoints[currentposition].ResponseOutcome[Option - 1];

        return activeDialogueTree.conversationPoints[currentposition];
     //this should be right
     if (Option == 0)
        {
            //pretty sure relation ship type is supposeed to be 0-2 so I don't think there is a 2.5 but ok
            if (activeDialogueTree.relationshipType > 2.5)
            {
                FollowerManager followers = GetComponent<FollowerManager>();
                followers.AddFollower(Npc);
                
            }
            else
            {
                
                setRelationshipType(-0.5f);
            }

        }
    }

    public string Closer()
    {
        return conversation.GoAway;
    }

    //modifies relationship type
    private void setRelationshipType(float change)
    {
        conversation.relationshipType += change; //Relationship Type? 
    }

  
    private Statement[] csvReader(TextAsset csvInfo)
    {
        string[][] Data = Parse(csvInfo);

        Statement[] ParsedData = new Statement[Data.Length];

        for (int y = 1; y < Data.Length; y++)
        {
            Debug.Log("Ping");
            string NpcLine = Data[y][1];
            string[] Response = new string[4];
            float[] ResponseModifier = new float[4];
            int[] ResponseOutcome = new int[4];

            for (int x = 2; x < Data[y].Length; x++)
            {
                switch((x - 2) % 3)
                {
                    case 0:
                        Response[(x - 2) / 3] = Data[y][x];
                        break;
                    case 1:
                        ResponseModifier[(x - 2) / 3] = float.Parse(Data[y][x]);
                        break;
                    case 2:
                        ResponseOutcome[(x - 2) / 3] = int.Parse(Data[y][x]);
                        break;
                    case 3:
                        Response[(x - 2) / 3] = Data[y][x];
                        break;
                }
            }

            ParsedData[y-1] = new Statement(NpcLine, Response, ResponseModifier, ResponseOutcome);
        }

        Debug.Log(ParsedData);

        return ParsedData;
    }

    public string[][] Parse(TextAsset csvInfo)
    {

        string Data = csvInfo.text;
        // CSVs deliminate rows using newlines, so we will first split along newlines to get the rows.
        string[] rows = Data.Split('\n');

        // Once we have raw rows, initialize the output array.
        string[][] output = new string[rows.Length][];

        // For each row, process each entry
        for (int rowIndex = 0; rowIndex < rows.Length; rowIndex++)
        {
            // Separate each column from each row.
            output[rowIndex] = SeparateColumnsOfRow(rows[rowIndex]);
        }

        return output;
    }

    public string[] SeparateColumnsOfRow(string row)
        {
            // Edge case: row is empty string
            if (row.Length == 0)
            {
                return new string[0];
            }

            // Create a collection for storing our finished records.
            List<string> finishedRecords = new List<string>();

            // Get underlying char array representation of string so we can
            // walk through it character-by-character
            char[] rowAsCharArray = row.ToCharArray();

            // Create a "holding buffer" to record the current record.
            // This allows us to build the actual record by ignoring escape characters;
            // something you couldn't do with a normal split / substring operation.
            char[] currentRecord = new char[rowAsCharArray.Length];
            int currentRecordLength = 0;

            // Declare variables to track state
            int currentIndex = 0;
            bool shouldIgnoreCommas = false;

            while (currentIndex < rowAsCharArray.Length)
            {
                char currentChar = rowAsCharArray[currentIndex];

                // Quote handling
                if (currentChar == '"')
                {
                    // If not escaped, this means we should toggle whether we are ignoring commas or not.
                    if (!IsDoubleQuoteEscaped(rowAsCharArray, currentIndex))
                    {
                        shouldIgnoreCommas = !shouldIgnoreCommas;
                    }
                    else
                    {
                        // This character is escaped by the next character. Add this character to the
                        // buffer, but then skip the next quote; the only purpose of the next quote is
                        // make this quote an actual quote and not an escape.
                        currentRecord[currentRecordLength++] = currentChar;
                        ++currentIndex;

                    }
                }
                // End of record handling
                // A record ends if we should not ignore commas and encounter one, we reach the end of the line, or we reach the end of the string.
                else if ( (!shouldIgnoreCommas && currentChar == ',') || currentChar == '\n')
                {
                    // Construct a record from the buffer
                    finishedRecords.Add(new string(currentRecord, 0, currentRecordLength));
                    currentRecordLength = 0; // Reset buffer size
                }
                else // Otherwise this is suitable to add to the current buffer.
                {
                    currentRecord[currentRecordLength++] = currentChar;
                }

                ++currentIndex;
            }

            // Finish a record if we left one unfinished
            if (currentRecordLength > 0)
            {
                // Construct a record from the buffer
                finishedRecords.Add(new string(currentRecord, 0, currentRecordLength));
                currentRecordLength = 0; // Reset buffer size
            }

            //Once done, move records from list to array
            return finishedRecords.ToArray();

        }

        /// <summary>
        /// Returns true if the double quote at the currentIndex position of the rowAsCharArray
        /// is escaped.
        /// </summary>
        /// <param name="rowAsCharArray"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private bool IsDoubleQuoteEscaped(char[] rowAsCharArray, int currentIndex)
        {
            // Return true only if the current character is a double quote, another character exists in the array, and that character is also a double quote.
            return rowAsCharArray[currentIndex] == '"' && ((currentIndex + 1) < rowAsCharArray.Length)  && rowAsCharArray[currentIndex + 1] == '"';
        }

    //public

}