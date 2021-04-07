using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerInterface : MonoBehaviour
{
    interface iFollowers
    {
        string name { get; set; }
        string specialty { get; set; }
        int level { get; set; }
        GameObject avatar { get; set; }
    }

    class Follower : iFollowers
    {
        public string name { get; set; }
        public string specialty { get; set; }
        public int level { get; set; }
        public GameObject avatar { get; set; }
        public Follower(string _name, string _specialty, int _level, GameObject _avatar)
        {
            name = _name;
            specialty = _specialty;
            level = _level;
            avatar = _avatar;
        }
    }
  
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
