using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorsNShi : MonoBehaviour
{
    private string sceneToLoad = null; // Stores the scene to load when 'L' is pressed

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered by: {other.gameObject.name}");

        // Check if the colliding object has a valid tag and set the scene to load
        switch (other.gameObject.tag)
        {
            case "Mirror":
                sceneToLoad = "Level 2";
                break;
            case "Mirror2":
                sceneToLoad = "Level 3";
                break;
            case "Mirror3":
                sceneToLoad = "Level 4";
                break;
            case "Mirror4":
                sceneToLoad = "Devroom";
                break;
            case "MirrorD":
                sceneToLoad = "Bingusroom";
                break;
            default:
                Debug.LogWarning($"No scene associated with tag: {other.gameObject.tag}");
                sceneToLoad = null; // Reset sceneToLoad if the tag is invalid
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset the sceneToLoad when the player exits the trigger zone
        if (other.CompareTag("Mirror") || other.CompareTag("Mirror2") || other.CompareTag("Mirror3") || other.CompareTag("Mirror4") || other.CompareTag("MirrorD"))
        {
            Debug.Log($"Exited trigger zone of: {other.gameObject.name}");
            sceneToLoad = null;
        }
    }

    private void Update()
    {
        // Check if 'L' is pressed and a valid scene is set to load
        if (Input.GetKeyDown(KeyCode.L) && sceneToLoad != null)
        {
            Debug.Log($"Loading scene: {sceneToLoad}");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
