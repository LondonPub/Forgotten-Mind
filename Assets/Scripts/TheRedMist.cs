using UnityEngine;
using System.Collections;

public class TheRedMist : MonoBehaviour
{
    public float detectionRange = 20f;
    public float attackRange = 1.5f;
    public float speed = 3f;
    public float wanderTime = 3f;
    public LayerMask HidingPlaceLayer;

    public Transform wanderPointA; 
    public Transform wanderPointB;

    private Transform player;
    private Transform monster;
    private Rigidbody monsterRigidbody;
    private bool isChasing = false;
    private bool isStunned = false;
    private yuri playerHidingScript;

    private Transform currentWanderTarget;

    private float timeSinceLastChase = 0f;
    private float maxIdleTime = 25f;
    private bool hasEverChased = false; // 🚨 Flag to check if chase has started at least once

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

        if (wanderPointA == null || wanderPointB == null)
        {
            Debug.LogError("Wander points A and B must be assigned in the Inspector!");
        }

        currentWanderTarget = wanderPointA;
    }

    private void Update()
    {
        if (player == null || monster == null || isStunned) return;

        float distanceToPlayer = Vector3.Distance(player.position, monster.position);
        bool playerIsHiding = playerHidingScript != null && playerHidingScript.IsPlayerHiding();

        if (playerIsHiding)
        {
            isChasing = false;
        }
        else if (distanceToPlayer < detectionRange)
        {
            isChasing = true;
            hasEverChased = true; // ✅ Set flag on first chase
            timeSinceLastChase = 0f; // Reset idle timer when chasing
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            monster.position = Vector3.MoveTowards(monster.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            WanderBetweenPoints();

            if (hasEverChased)
            {
                timeSinceLastChase += Time.deltaTime;

                if (timeSinceLastChase >= maxIdleTime)
                {
                    Destroy(monster.gameObject);
                    Debug.Log("Monster destroyed due to inactivity after chasing.");
                }
            }
        }

        if (isChasing && distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }
    }

    private void WanderBetweenPoints()
    {
        if (currentWanderTarget == null) return;

        monster.position = Vector3.MoveTowards(monster.position, currentWanderTarget.position, speed * Time.deltaTime);

        if (Vector3.Distance(monster.position, currentWanderTarget.position) < 0.5f)
        {
            currentWanderTarget = currentWanderTarget == wanderPointA ? wanderPointB : wanderPointA;
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

        if (monsterRigidbody != null)
        {
            monsterRigidbody.constraints = RigidbodyConstraints.FreezeAll; 
        }

        Debug.Log("HeLeNa? is stunned for " + duration + " seconds.");
        yield return new WaitForSeconds(duration);

        if (monsterRigidbody != null)
        {
            monsterRigidbody.constraints = RigidbodyConstraints.None; 
        }

        isStunned = false;
        Debug.Log("HeLeNa? can move again");
    }
}
