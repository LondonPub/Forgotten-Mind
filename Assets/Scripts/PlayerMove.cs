//Thomas H.A. Arruda
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{ 
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    public Animator animator;

    float horizontalMovement = 0f;

    public float maxVelocity = 25f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Code for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        horizontalMovement = horizontalInput * moveSpeed;
        Vector2 moveVector = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        rb.velocity = moveVector;


        // Clamp velocity to the max value
        if(rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }
}