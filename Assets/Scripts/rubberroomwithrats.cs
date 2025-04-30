using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // To access UI elements like Slider

public class RubberRoomWithRats : MonoBehaviour
{
    public float maxSanity = 100f; // Maximum sanity value
    public float currentSanity; // Current sanity value
    public Slider sanitySlider; // Reference to a UI Slider (used for visual representation)

    private void Start()
    {
        // Set the initial sanity level
        currentSanity = maxSanity;
        
        // Set the Slider's max and current value
        sanitySlider.maxValue = maxSanity;
        sanitySlider.value = currentSanity;
    }

    private void Update()
    {
        // Optionally, you could add functionality to gradually regenerate sanity
        // For now, we'll just focus on decreasing sanity on taking damage
    }

    // Method to decrease sanity when the player is attacked
    public void DecreaseSanity(float amount)
    {
        currentSanity -= amount;
        if (currentSanity < 0)
        {
            currentSanity = 0;
        }

        // Update the slider to reflect the current sanity
        sanitySlider.value = currentSanity;

        // Optionally: Implement a game-over or other effect when sanity reaches 0
        if (currentSanity <= 0)
        {
            HandleSanityDepletion();
        }
    }

    // Method to handle the situation when sanity reaches 0
    private void HandleSanityDepletion()
    {
        // Example of what happens when sanity depletes:
        // You can trigger a game-over or other mechanic (like changing the scene or triggering an animation)
        Debug.Log("Sanity depleted! Game Over or some other consequence.");
        // Trigger some event, for example: GameManager.Instance.GameOver();
    }
}
