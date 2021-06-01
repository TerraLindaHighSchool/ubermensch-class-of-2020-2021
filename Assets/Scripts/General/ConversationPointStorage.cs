using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPointStorage : MonoBehaviour
{
    public GameObject conversationTransform;

    public Transform NpcFace()
    {
        return conversationTransform.transform;
    }
}
