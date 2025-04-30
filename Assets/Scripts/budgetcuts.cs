using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDisappear : MonoBehaviour
{
    public float viewTimeThreshold = 3f; // Time the monster must be seen before disappearing
    public Transform playerCamera;       // Assign this to the player's camera in the inspector
    public float viewAngleThreshold = 60f; // Field of view threshold in degrees

    private float timeSeen = 0f;
    private Renderer monsterRenderer;
    private bool hasDisappeared = false;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;
        }

        monsterRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (hasDisappeared)
            return;

        if (IsSeenByPlayer())
        {
            timeSeen += Time.deltaTime;

            if (timeSeen >= viewTimeThreshold)
            {
                Disappear();
            }
        }
        else
        {
            timeSeen = 0f; // Reset if no longer seen
        }
    }

    bool IsSeenByPlayer()
    {
        Vector3 directionToMonster = transform.position - playerCamera.position;
        float angle = Vector3.Angle(playerCamera.forward, directionToMonster);

        if (angle > viewAngleThreshold)
            return false;

        Ray ray = new Ray(playerCamera.position, directionToMonster.normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                return true;
            }
        }

        return false;
    }

    void Disappear()
    {
        hasDisappeared = true;
        gameObject.SetActive(false); // Or use Destroy(gameObject) if permanent
        Debug.Log("Monster disappeared after being seen for 3 seconds.");
    }
}

