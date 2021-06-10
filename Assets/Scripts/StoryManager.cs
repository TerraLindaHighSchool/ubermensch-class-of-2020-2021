using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<TutorialController>().tutorialLoader(0);
    }
}
