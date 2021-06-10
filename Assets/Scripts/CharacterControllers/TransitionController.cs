using UnityEngine;
using UnityEngine.SceneManagement;

// This script is attached to the Player prefab
public class TransitionController : MonoBehaviour
{
    [SerializeField] public GameObject playerModel;

    int jankMoveFix = 0;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("DontDestroy"))
        {
            DontDestroyOnLoad(i);
        }
        /*
         * This breaks things??
         * 
         * 
        if (SceneManager.GetActiveScene().name == "HomeBase_UnderSubway")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<FollowerTrader>().PartyInTheHomeBase();
        }
        */
    }

    public void DontDestroy(GameObject i)
    {
        DontDestroyOnLoad(i);
    }

    /* Each portal will be an IPortal ScriptableObject containing a scene name, transitionPoint
     * and the inventory required to transition.  SceneLoader will be called from the Interact 
     * method of the PlayerController after triggering the portal and pressing 'E'.
     */

    private bool needsSeattleTutorial = true;
    private bool needsBeyondTutorial = true;

    public void SceneLoader(string scene, Vector3 destination)
    {
        //SceneMusic(scene);
        playerModel = transform.Find("PlayerModel").gameObject;
        playerModel.GetComponent<MovementController>().enabled = false;
        Debug.Log("Player Location before Change: " + playerModel.transform.position);
        transform.position = Vector3.zero; //This is the Player (Clone)
        Debug.Log(playerModel.GetInstanceID());
        playerModel.transform.localPosition = transform.InverseTransformPoint(destination);
        SceneManager.LoadScene(scene);
        Debug.Log("Player Location after Change: " + playerModel.transform.position);
        Debug.Log(destination);

        string mission;

        switch(scene)
        {
            case "HomeBase":
                mission = "Find the arc of life, and bring as many people with you as you can";
                break;
            case "Seattle":
                mission = "Get the boat key from Lady Bisco and go to the wasteland, from there you may be able to find clues to the whereabouts of the arc of life.";
                if (needsSeattleTutorial)
                {
                    GameObject.Find("GameManager").GetComponent<TutorialController>().tutorialLoader(1);
                    needsSeattleTutorial = false;
                }
                break;
            case "Lab Land":
                mission = "Find and search labs for clues about the Arc of Life.";
                break;
            case "WastelandTerrain":
                mission = "Get oxygen and find your way to the Lab Lands.";
                mission = "Get the boat key from Lady Bisco and go to the wasteland, from there you may be able to find clues to the whereabouts of the arc of life.";
                if (needsBeyondTutorial)
                {
                    GameObject.Find("GameManager").GetComponent<TutorialController>().tutorialLoader(2);
                    needsBeyondTutorial = false;
                }
                break;
            case "Lab 1 Interior":
                mission = "Find clues about the Arc of Life.";
                break;
            case "Lab 2 Interior":
                mission = "Find clues about the Arc of Life.";
                break;
            case "Lab 3 Interior":
                mission = "Find clues about the Arc of Life.";
                break;
            case "arcoflife":
                mission = "Climb the mountain. What you seek lies there.";
                break;
            case "EnemyCompound":
                mission = "Locate the Files with the location of the arc of life.";
                break;
            default:
                mission = "Find the arc of life, and bring as many people with you as you can";
                break;
        }

        playerModel.GetComponent<PlayerController>().currentMission = mission;
    }

    private void Update()
    {
        if(playerModel.GetComponent<MovementController>().enabled == false && jankMoveFix > 10)
        {
            playerModel.GetComponent<MovementController>().enabled = true;
            jankMoveFix = 0;
        } else if(playerModel.GetComponent<MovementController>().enabled == false)
        {
            jankMoveFix++;
        }
    }

    /*
    private void SceneMusic(string scene)
    {
        MusicController musicController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MusicController>();
        switch (scene)
        {
            case "HomeBase_UnderSubway":
                musicController.TrackSwitch(2);
                break;
            case "Seattle":
                musicController.TrackSwitch(3);
                break;
            case "arcoflife":
                musicController.TrackSwitch(4);
                break;
            default:
                musicController.TrackSwitch(0);
                break;
        }
    }
    */

    public void exitButton() //used for exit button
    {
        this.GetComponentInChildren<PlayerController>().exitTrade();
    }
}