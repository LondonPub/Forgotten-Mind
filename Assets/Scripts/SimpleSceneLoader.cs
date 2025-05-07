using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.Log("Loading scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " cannot be loaded. Check if it is added to the Build Settings.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Press 'L' to load the next scene
        {
            string currentScene = SceneManager.GetActiveScene().name; // Get the current scene name
            string nextScene = GetNextScene(currentScene); // Determine the next scene to load

            if (!string.IsNullOrEmpty(nextScene))
            {
                Debug.Log($"Current scene: {currentScene}, loading next scene: {nextScene}");
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                Debug.LogWarning($"No next scene defined for the current scene: {currentScene}");
            }
        }
    }

    private string GetNextScene(string currentScene)
    {
        // Map current scenes to their corresponding next scenes
        switch (currentScene)
        {
            case "Level 1":
                return "Level 2";
            case "Level 2":
                return "Level 3";
            case "Level 3":
                return "Level 4";
            case "Level 4":
                return "Dev Room";
            case "Dev Room":
                return "bingus Room";
            default:
                return null; // No next scene defined
        }
    }
}