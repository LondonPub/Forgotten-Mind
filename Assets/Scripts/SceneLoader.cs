using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (sceneLoader == null)
        {
            Debug.LogError("SceneLoader not found in the scene!");
        }
        else
        {
            Debug.Log("SceneLoader successfully assigned.");
        }
    }

    public void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " cannot be loaded. Check if the scene is added to the build settings.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mirror"))
        {
            Debug.Log("Loading scene: Level 2");
            LoadScene("Level 2");
        }
        else if (other.gameObject.CompareTag("Mirror2"))
        {
            Debug.Log("Loading scene: Level 3");
            LoadScene("Level 3");
        }
    }
}
