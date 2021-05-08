using UnityEngine;
using UnityEngine.SceneManagement;

// This script is attached to the Player prefab
public class TransitionController : MonoBehaviour
{
    [SerializeField] public GameObject playerModel;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /* Each portal will be an IPortal ScriptableObject containing a scene name, transitionPoint
     * and the inventory required to transition.  SceneLoader will be called from the Interact 
     * method of the PlayerController after triggering the portal and pressing 'E'.
     */
    
    public void SceneLoader(string scene, Vector3 destination)
    {
        transform.position = Vector3.zero;
        playerModel.transform.position = destination;
        SceneManager.LoadScene(scene);
    }
}
