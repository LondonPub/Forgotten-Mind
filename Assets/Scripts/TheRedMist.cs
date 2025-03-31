using UnityEngine;
using System.Collections;

public class TheRedMist : MonoBehaviour
{
    public float detectionRange = 20f;
    public float attackRange = 1.5f;
    public float speed = 3f;
    public LayerMask HidingPlaceLayer;

    private Transform player;
    private Transform monster;
    private Rigidbody monsterRigidbody; // To store the monster's Rigidbody component
    private bool isChasing = false;
    private bool isStunned = false;
    private yuri playerHidingScript; // Reference to the yuri script to check if the player is hiding

    private void Start()
    {
        GameObject monsterObj = GameObject.FindGameObjectWithTag("Schizo");
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (monsterObj != null)
        {
            monster = monsterObj.transform;
            monsterRigidbody = monsterObj.GetComponent<Rigidbody>(); // Get the Rigidbody component
        }
        else
        {
            Debug.LogError("Monster with tag 'Schizo' not found!");
        }

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHidingScript = playerObj.GetComponent<yuri>(); // Get the yuri script from the player
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }
    }

    private void Update()
    {
        if (player == null || monster == null || isStunned) return;

        float distanceToPlayer = Vector3.Distance(player.position, monster.position);

        // Check if the player is hiding using the yuri script
        bool playerIsHiding = playerHidingScript != null && playerHidingScript.IsPlayerHiding();

        // Stop chasing if the player is hiding
        if (playerIsHiding)
        {
            isChasing = false;
        }
        // Check if the player is within detection range and not hiding
        else if (distanceToPlayer < detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // Move the monster towards the player if chasing
        if (isChasing)
        {
            monster.position = Vector3.MoveTowards(monster.position, player.position, speed * Time.deltaTime);
        }

        // If the monster is close enough, attack the player
        if (isChasing && distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("HeLeNa? got too close");
        StartCoroutine(StunMonster(10f));
    }

    private IEnumerator StunMonster(float duration)
    {
        isStunned = true;
        isChasing = false;

        // Lock the movement and rotation of the monster while it's stunned
        if (monsterRigidbody != null)
        {
            monsterRigidbody.constraints = RigidbodyConstraints.FreezeAll; // Freeze both movement and rotation
        }

        Debug.Log("HeLeNa? is stunned for " + duration + " seconds.");
        yield return new WaitForSeconds(duration);

        // Unlock the movement and rotation after the stun duration is over
        if (monsterRigidbody != null)
        {
            monsterRigidbody.constraints = RigidbodyConstraints.None; // Allow movement and rotation again
        }

        isStunned = false;
        Debug.Log("HeLeNa? can move again");
    }
}
