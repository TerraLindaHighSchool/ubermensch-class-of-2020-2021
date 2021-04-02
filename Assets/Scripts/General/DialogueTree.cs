using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//not sure what monobehaviour is for but it got mad at me when it wasn't there
public class DialogueTree : MonoBehaviour
{
    //make a string instead of the thingy 
    //so you can use the code on all npcs 
    //cause it's public its availbe in inspector
    public string CSVFileName;
  
    void Start()
    {
        //hopefully this is right
        TextAsset csvInformation = Resources.Load<TextAsset>("Dilogue_GruceBustin");
        
        // \n is the thingy for new line
        string[] data = csvInformation.text.Split(new char[] { '\n' });

        //this is assuming there is the collumn labelly things in the first row and nothing in the very last row
        for (int i = 0; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
        }
        
    }

    void Update()
    {

    }

}