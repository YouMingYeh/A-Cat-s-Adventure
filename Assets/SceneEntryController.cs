using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntryController : MonoBehaviour
{
    // Start is called before the first frame update
    public string initialSceneName = "MenuScene";  // The name of the scene you want to be active at the start.

    void Start()
    {
        // Load the initial scene non-additively (unloads the current scene).
        
        SceneManager.LoadScene(initialSceneName);
    }

    public void LoadScene(string sceneName)
    {
        // Load a new scene, replacing the currently active scene.
        SceneManager.LoadScene(sceneName);
    }
}
