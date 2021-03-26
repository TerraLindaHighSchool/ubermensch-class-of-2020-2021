using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private GameObject activeHUD;
    public GameObject[] HUDs;
    public void HUDLoader(int hud, GameObject caller)
    {
        HUDs[hud].SetActive(true);
    }
    public void HUDDeLoader(int hud)
    {
        HUDs[hud].SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
