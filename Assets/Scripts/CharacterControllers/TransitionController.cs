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
    }

    public void DontDestroy(GameObject i)
    {
        DontDestroyOnLoad(i);
    }

    /* Each portal will be an IPortal ScriptableObject containing a scene name, transitionPoint
     * and the inventory required to transition.  SceneLoader will be called from the Interact 
     * method of the PlayerController after triggering the portal and pressing 'E'.
     */
    
    public void SceneLoader(string scene, Vector3 destination)
    {
        SceneMusic(scene);
        playerModel = transform.Find("PlayerModel").gameObject;
        playerModel.GetComponent<MovementController>().enabled = false;
        Debug.Log("Player Location before Change: " + playerModel.transform.position);
        transform.position = Vector3.zero; //This is the Player (Clone)
        Debug.Log(playerModel.GetInstanceID());
        playerModel.transform.localPosition = transform.InverseTransformPoint(destination);
        SceneManager.LoadScene(scene);
        Debug.Log("Player Location after Change: " + playerModel.transform.position);
        Debug.Log(destination);
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

    public void exitButton() //used for exit button
    {
        this.GetComponentInChildren<PlayerController>().exitTrade();
    }
}