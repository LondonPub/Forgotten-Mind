using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorsNShi : MonoBehaviour
{
    private SceneLoader sceneLoader; // Reference to the SceneLoader script

    private void Start()
    {
        // Find the SceneLoader in the scene
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (sceneLoader == null)
        {
            Debug.LogError("SceneLoader not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);

        if (sceneLoader == null) return; // Ensure SceneLoader is assigned

        if (other.gameObject.CompareTag("Mirror"))
        {
            Debug.Log("Loading scene: Level 2");
            sceneLoader.LoadScene("Level 2");
        }
        else if (other.gameObject.CompareTag("Mirror2"))
        {
            Debug.Log("Loading scene: Level 3");
            sceneLoader.LoadScene("Level 3");
        }
        else if (other.gameObject.CompareTag("Mirror3"))
        {
            Debug.Log("Loading scene: Level 4");
            sceneLoader.LoadScene("Level 4");
        }
        else if (other.gameObject.CompareTag("Win"))
        {
            Debug.Log("Loading scene: Winscreen");
            sceneLoader.LoadScene("Winscreen");
        }
        else if (other.gameObject.CompareTag("Devroom"))
        {
            Debug.Log("Loading scene: Devroom");
            sceneLoader.LoadScene("Devroom");
        }
        else if (other.gameObject.CompareTag("Bingus"))
        {
            Debug.Log("Loading scene: Bingusroom");
            sceneLoader.LoadScene("Bingusroom");
        }
    }
}
