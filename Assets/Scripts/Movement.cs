using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator; // Reference to the Animator

    public float moveSpeed = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Set the velocity of the Rigidbody
        rb.velocity = new Vector2(horizontalInput, verticalInput) * moveSpeed;

        // Calculate the speed (magnitude of movement)
        float speed = rb.velocity.magnitude;

        // Update the Animator's Speed parameter
        animator.SetFloat("Speed" , Mathf.Abs(horizontalInput));

        // Update the Animator's Horizontal parameter
        animator.SetFloat("Horizontal", horizontalInput);

        // Flip the player sprite based on horizontal movement
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
    }
}
