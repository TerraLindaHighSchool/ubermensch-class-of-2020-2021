using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DialogueTree
{
    interface DialogueTree
    {
        float relationshipType { get; set; }

        Statement[] conversationPoints { get; } 
    }
}