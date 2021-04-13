using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{

    private DialogueTree conversation;
    private DialogueTree combat;
    //not sure what this does
    private int currentposition;

    public void OnEnable()
    {

    }

    public void StartConversation()
    {
        //plays the opener?
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
    private Statement[] csvReader(TextAsset csvInfo)
    {

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
