using UnityEngine;
using System.Collections;

public class TheRedMist : MonoBehaviour
{
    public float detectionRange = 20f;
    public float attackRange = 1.5f;
    public float speed = 3f;
    public float wanderRange = 10f; // The range in which the monster will wander
    public float wanderTime = 3f; // Time spent wandering before choosing another point
    public LayerMask HidingPlaceLayer;

    private Transform player;
    private Transform monster;
    private Rigidbody monsterRigidbody;
    private bool isChasing = false;
    private bool isStunned = false;
    private yuri playerHidingScript;

    private Vector3 wanderTarget; // The current target position for wandering
    private float wanderTimer = 1f;

    private void Start()
    {
        GameObject monsterObj = GameObject.FindGameObjectWithTag("Schizo");
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (monsterObj != null)
        {
            monster = monsterObj.transform;
            monsterRigidbody = monsterObj.GetComponent<Rigidbody>(); 
        }
        else
        {
            Debug.LogError("Monster with tag 'Schizo' not found!");
        }

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHidingScript = playerObj.GetComponent<yuri>();
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }

        // Initialize the wander target
        SetNewWanderTarget();
    }

    private void Update()
    {
        if (player == null || monster == null || isStunned) return;

        float distanceToPlayer = Vector3.Distance(player.position, monster.position);

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

        // If chasing, move towards the player
        if (isChasing)
        {
            monster.position = Vector3.MoveTowards(monster.position, player.position, speed * Time.deltaTime);
        }
        // If not chasing, wander around the area
        else
        {
            Wander();
        }

        // If close enough to the player, attack
        if (isChasing && distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }
    }

    private void Wander()
    {
        // Move towards the current wander target
        monster.position = Vector3.MoveTowards(monster.position, wanderTarget, speed * Time.deltaTime);

        // If the monster reaches the wander target, pick a new one
        if (Vector3.Distance(monster.position, wanderTarget) < 0.5f)
        {
            SetNewWanderTarget();
        }
    }

    private void SetNewWanderTarget()
    {
        // Get a random point within the wander range
        float randomX = Random.Range(-wanderRange, wanderRange);
        float randomZ = Random.Range(-wanderRange, wanderRange);

        // Set the target position
        wanderTarget = new Vector3(monster.position.x + randomX, monster.position.y, monster.position.z + randomZ);

        // Optionally, you could add boundary checks here if you want to limit wandering within a specific area.
        // For example, clamping the target position to a certain area.
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
            monsterRigidbody.constraints = RigidbodyConstraints.FreezeAll; 
        }

        Debug.Log("HeLeNa? is stunned for " + duration + " seconds.");
        yield return new WaitForSeconds(duration);

        // Unlock the movement and rotation after the stun duration is over
        if (monsterRigidbody != null)
        {
            monsterRigidbody.constraints = RigidbodyConstraints.None; 
        }

        isStunned = false;
        Debug.Log("HeLeNa? can move again");
    }
}
