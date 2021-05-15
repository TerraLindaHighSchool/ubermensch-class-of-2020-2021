using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
[CreateAssetMenu(fileName = "new Standard Portal", menuName = "Resources/Portal", order = 1)]

public class StandardPortal : ScriptableObject
{
    public string scene;
    public Vector3 destination;
    public StandardInventoryItem[] exitRequirements;
    public float minutesToConsumeFood;
    public float minutesToConsumeOxygen;

    public string Scene => scene;
    public Vector3 Destination => destination;
    public StandardInventoryItem[] ExitRequirements => exitRequirements;
    
    // minutes to consume assumes food level at 100% (1.0);
    public float MinutesToConsumeFood => minutesToConsumeFood;
    public float MinutesToConsumeOxygen => minutesToConsumeOxygen;
}
