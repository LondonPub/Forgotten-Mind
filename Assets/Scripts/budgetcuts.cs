using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMS : MonoBehaviour
{
    public float viewTimeThreshold = 3f; // Seconds the monster must be visible before disappearing
    private float timeSeen = 0f;
    private bool hasDisappeared = false;

    private Camera mainCamera;
    private Renderer monsterRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        monsterRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (hasDisappeared)
            return;

        if (IsVisibleToMainCamera())
        {
            timeSeen += Time.deltaTime;
            if (timeSeen >= viewTimeThreshold)
            {
                Disappear();
            }
        }
        else
        {
            timeSeen = 0f; // Reset timer if not seen
        }
    }

    bool IsVisibleToMainCamera()
    {
        if (!monsterRenderer.isVisible)
            return false;

        // Confirm the monster is actually in view of the *main* camera using viewport space
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

        bool inView = viewportPoint.z > 0 && // In front of camera
                      viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                      viewportPoint.y >= 0 && viewportPoint.y <= 1;

        return inView;
    }

    void Disappear()
    {
        hasDisappeared = true;
        gameObject.SetActive(false); // Or use Destroy(gameObject) for permanent removal
        Debug.Log("Monster disappeared after being seen by the main camera for 3 seconds.");
    }
}
