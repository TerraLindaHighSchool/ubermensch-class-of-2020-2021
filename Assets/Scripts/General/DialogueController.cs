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

    //test

    public TextAsset testAsset;

    //find dialoguetree and combat tree and assign them to conversation and combat respectiveley respectively
    //isn't there supposed to be like a public string thingy?
    public void OnEnable()
    {

    }

    //for start conversation tree? (load the first item in the start conversation tree)
    public void StartConversation()
    {
        
    }

    public void StartCombat()
    {
        //plays the other opener?
    }

    public void LoadNext(int Option)
    {
        //I think it's supposed to keep track of the options?

    }

    //modifies relationship type
    private void setRelationshipType(float change)
    {

    }

    //csvinformation? statement is return type
    //I think this is where the magic happens

    /*
    private Statement[] csvReader(TextAsset csvInfo)
    {
        
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
