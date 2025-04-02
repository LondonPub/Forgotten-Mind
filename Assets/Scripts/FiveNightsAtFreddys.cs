using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIVENIGHTSATFREDDYS : MonoBehaviour
{
    // The sprite you want to switch to
    public Sprite newSprite;

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    // Scale factor for enlarging the sprite
    public Vector3 enlargedScale = new Vector3(6f, 6f, 1f); // Example: enlarge the sprite

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb2d;

    // Time in seconds before the sprite disappears and the position resets
    public float disappearTime = 5f;

    // The target position to move the Player to after 5 seconds
    public Vector3 targetPosition;

    // Store the original sprite and position
    private Sprite originalSprite;
    private Vector3 originalPosition;

    // The position to move the player when colliding with "Schizo"
    public Vector3 playerTargetPosition;

    // Time to freeze the transform (movement and rotation) after sprite change
    public float freezeDuration = 10f;

    void Start()
    {
        // Get the SpriteRenderer component of the object
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the Rigidbody2D component of the object
        rb2d = GetComponent<Rigidbody2D>();

        // Store the original sprite and position
        originalSprite = spriteRenderer.sprite;
        originalPosition = transform.position;
    }

    // This function is called when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if this object has the "Schizo" tag
            if (gameObject.CompareTag("Schizo"))
            {
                // Check if there is a valid sprite renderer and a valid new sprite
                if (spriteRenderer != null && newSprite != null)
                {
                    // Change the sprite of this object to the new sprite
                    spriteRenderer.sprite = newSprite;

                    // Enlarge the sprite by setting its scale
                    transform.localScale = enlargedScale;

                    // Position the sprite at the center of the screen (world coordinates)
                    transform.position = new Vector3(0f, 0f, transform.position.z); // Center in world space

                    // Set gravity scale to 0 to stop gravity effects
                    if (rb2d != null)
                    {
                        rb2d.gravityScale = 0;
                    }

                    // Lock the movement and rotation by setting the Rigidbody2D body type to Kinematic
                    if (rb2d != null)
                    {
                        rb2d.bodyType = RigidbodyType2D.Kinematic;
                        rb2d.velocity = Vector2.zero; // Optionally set velocity to zero
                    }

                    // Start the coroutine to disappear, reset the sprite/position, and then move the player
                    StartCoroutine(FreezeAndReset(collision.gameObject));
                }
            }
        }
    }

    // Coroutine to handle freezing, disappearing, resetting the sprite and position, and then moving the player
    private IEnumerator FreezeAndReset(GameObject player)
    {
        // Wait for the specified freeze duration (10 seconds)
        yield return new WaitForSeconds(freezeDuration);

        // After 10 seconds, unfreeze the transform (movement and rotation)
        if (rb2d != null)
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic; // Set the Rigidbody2D back to Dynamic to enable movement and rotation
        }

        // Wait for the disappearTime duration
        yield return new WaitForSeconds(disappearTime);

        // Disable the sprite renderer to make the image disappear
        spriteRenderer.enabled = false;

        // Reset the sprite back to the original sprite
        spriteRenderer.sprite = originalSprite;

        // Reset the position back to the original position
        transform.position = originalPosition;

        // Reset the scale to the original size (optional)
        transform.localScale = Vector3.one;

        // Reset Rigidbody2D properties if needed
        if (rb2d != null)
        {
            rb2d.gravityScale = 1; // Reset gravity scale to its default
            rb2d.bodyType = RigidbodyType2D.Dynamic; // Ensure Rigidbody2D is dynamic after reset
        }

        // Move the player to the specified target position (immediately after the reset)
        if (player != null)
        {
            player.transform.position = playerTargetPosition;
        }

        // Enable the sprite renderer back again (after player has moved)
        spriteRenderer.enabled = true;
    }
}
