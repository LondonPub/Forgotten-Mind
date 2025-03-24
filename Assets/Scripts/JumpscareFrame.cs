using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndReveal : MonoBehaviour
{
    // Reference to the SpriteRenderer component
    public SpriteRenderer spriteToReveal;

    // Scale factor for enlarging the sprite
    public Vector3 enlargedScale = new Vector3(2f, 2f, 1f);  // Example: Scale the sprite by 2x

    // Start is called before the first frame update
    void Start()
    {
        // Initially hide the sprite
        if (spriteToReveal != null)
        {
            spriteToReveal.enabled = false;
        }
    }

    // This function is called when a collision happens
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with has the tag "RevealTrigger"
        if (collision.gameObject.CompareTag("RevealTrigger"))
        {
            // Reveal the sprite and position it at the center of the screen
            if (spriteToReveal != null)
            {
                spriteToReveal.enabled = true;

                // Enlarge the sprite by setting its scale
                spriteToReveal.transform.localScale = enlargedScale;

                // Position the sprite at the center of the screen
                spriteToReveal.transform.position = new Vector3(0f, 0f, spriteToReveal.transform.position.z);
            }
        }
    }
}