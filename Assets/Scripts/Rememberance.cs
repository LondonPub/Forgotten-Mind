using UnityEngine;

public class Rememberance : MonoBehaviour
{
    [Tooltip("Target position to teleport the player to")]
    public Transform targetPosition;

    public void TeleportPlayer(GameObject player)
    {
        if (targetPosition != null && player != null)
        {
            player.transform.position = targetPosition.position;
        }
        else
        {
            Debug.LogWarning("Target position or player is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player GameObject is tagged as "Player"
        {
            TeleportPlayer(other.gameObject);
        }
    }
}
