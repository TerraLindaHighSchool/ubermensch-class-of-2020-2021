using UnityEngine;
using UnityEngine.SceneManagement;

// This script is attached to the Player prefab
public class TransitionController : MonoBehaviour
{
    [SerializeField] public GameObject playerModel;
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
        playerModel = transform.Find("PlayerModel").gameObject;

        Debug.Log("Player Location before Change: " + playerModel.transform.position);
        transform.position = Vector3.zero; //This is the Player (Clone)
        Debug.Log(playerModel.GetInstanceID());
        playerModel.transform.localPosition = transform.InverseTransformPoint(destination);
        SceneManager.LoadScene(scene);
        Debug.Log("Player Location after Change: " + playerModel.transform.position);
        Debug.Log(destination);
        inSeattle = true;
    }

    bool inSeattle = false;

    private void Update()
    {
        if(inSeattle)
        {
            Debug.Log("Player Location in Seattle: " + playerModel.transform.position);
        }
    }
}
