using UnityEngine;
using UnityEngine.AI;

public class AggressiveMonster : MonoBehaviour
{
    public float detectionRange = 20f; // Distance at which the monster will notice the player
    public float attackRange = 1.5f;   // Distance at which the monster will collide with the player
    public float speed = 3f;           // Monster's movement speed
    public Transform player;           // Reference to the player's transform
    public Transform monster;          // Reference to the monster's transform
    public LayerMask hidingPlaceLayer; // Layer to check for hiding places
    
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component
    private bool isChasing = false;    // Whether the monster is chasing the player

    private void Start()
    {
        // Get the NavMeshAgent component attached to the monster
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    private void Update()
    {
        // Check if the player is within detection range and not hidden
        if (Vector3.Distance(player.position, monster.position) < detectionRange && !IsPlayerHiding())
        {
            // Start chasing the player
            isChasing = true;
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            // Stop chasing the player if out of range or hiding
            isChasing = false;
            navMeshAgent.ResetPath();
        }

        // If the monster is chasing, check if it's close enough to attack
        if (isChasing && Vector3.Distance(player.position, monster.position) < attackRange)
        {
            // Here you can add code to attack the player (e.g., reduce health)
            AttackPlayer();
        }
    }

    private bool IsPlayerHiding()
    {
        // Check if the player is inside a hiding place (colliding with a "HidingPlace")
        Collider[] hidingPlaceColliders = Physics.OverlapSphere(player.position, 0.5f, hidingPlaceLayer);
        return hidingPlaceColliders.Length > 0;
    }

    private void AttackPlayer()
    {
        // Add logic for the monster attacking the player (e.g., reduce health, etc.)
        Debug.Log("Helena? has attacked the you!");
        // For now, we'll just log it for debugging
    }
}
