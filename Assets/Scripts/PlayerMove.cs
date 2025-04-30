//Thomas H.A. Arruda
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingNShi : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator; // Reference to the Animator

    public float moveSpeed = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component
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
        animator.SetFloat("Speed", speed);

        // Flip the player sprite based on horizontal movement
        Vector3 currentScale = transform.localScale; // Get the current scale
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // Face right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // Face left
        }
    }
}
