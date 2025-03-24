using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTargetPositionOnCheckpoint : MonoBehaviour
{
    // Reference to the ChangeSpriteOnCollision script
    private ChangeSpriteOnCollision changeSpriteScript;

    void Start()
    {
        // Get the ChangeSpriteOnCollision component from the object
        changeSpriteScript = GetComponent<ChangeSpriteOnCollision>();
    }

    // This function is called when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if this object has the "Checkpoint" tag
            if (gameObject.CompareTag("Checkpoint"))
            {
                // If the ChangeSpriteOnCollision script is found, update the target position
                if (changeSpriteScript != null)
                {
                    // Update the target position of the ChangeSpriteOnCollision script to the position of the checkpoint
                    changeSpriteScript.targetPosition = transform.position;
                }
            }
        }
    }
}
