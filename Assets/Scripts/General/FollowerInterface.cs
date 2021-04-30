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
        int npcStrength { get; set; }
        int npcCharisma { get; set; }
        int npcConstitution { get; set; }
    }

    public class Follower : iFollowers
    {
        public string name { get; set; }
        public string specialty { get; set; }
        public int level { get; set; }
        public GameObject avatar { get; set; }
        public int npcStrength { get; set; }
        public int npcCharisma { get; set; }
        public int npcConstitution { get; set; }
        public Follower(string _name, string _specialty, int _level, GameObject _avatar, int _npcStrength, int _npcCharisma, int _npcConstitution)
        {
            name = _name;
            specialty = _specialty;
            level = _level;
            avatar = _avatar;
            npcStrength = _npcStrength;
            npcCharisma = _npcCharisma;
            npcConstitution = _npcConstitution; 
        }
    }
}
