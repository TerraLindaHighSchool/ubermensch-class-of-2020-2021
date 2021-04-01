using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree
{
    void Start()
    {
        //hopefully this is right
        TextAsset csvInformation = Resources.Load<TextAsset>("Dilogue_GruceBustin");

        string[] data = csvInformation.text.Split(new char[] { "\n" });

        Debug.Log(data.Length);
        
    }

    void Update()
    {

    }

}