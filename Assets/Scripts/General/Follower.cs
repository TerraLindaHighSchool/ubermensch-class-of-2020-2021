using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public FollowerIdentity identity; 

    // Start is called before the first frame update
    void Start()
    {
        identity =  new FollowerIdentity("testFollowr", 3, Resources.Load<GameObject>("Assets/Resources/Icons/stick_noun_002_35886.jpg"), "look", 5, 4, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
