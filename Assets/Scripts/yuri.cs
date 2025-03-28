using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    private bool playerIsHiding = false;

    // This method handles the player entering a hiding place
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HidingPlace"))
        {
            playerIsHiding = true;
            Debug.Log("Player is hiding!");
        }
    }

    // This method handles the player leaving the hiding place
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HidingPlace"))
        {
            playerIsHiding = false;
            Debug.Log("Player is no longer hiding!");
        }
    }

    // Public method to allow other scripts to check if the player is hiding
    public bool IsPlayerHiding()
    {
        return playerIsHiding;
    }
}

