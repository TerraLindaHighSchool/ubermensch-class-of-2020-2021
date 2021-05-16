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
    public AudioClip[] sceneMusic;
    public float oxygenDepleteRate;

    public string Scene => scene;
    public Vector3 Destination => destination;
    public StandardInventoryItem[] ExitRequirements => exitRequirements;

    public AudioClip[] SceneMusic => sceneMusic;

    // minutes to consume assumes oxygen level if starting at 100% (1.0);
    public float OxygenDepleteRate => oxygenDepleteRate;
}