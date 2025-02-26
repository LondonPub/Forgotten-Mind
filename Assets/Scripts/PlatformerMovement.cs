//Thomas H.A. Arruda
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{ 
        public float moveSpeed = 5f;

        public float jumpForce = 10f;

        private bool isJumping = false;

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
        //Code for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;
        Vector2 moveVector = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        if(Input.GetButtonDown("Jump") && !isJumping){
            moveVector.y = jumpForce;
            isJumping = true;
            animator.SetBool("isJumping" , true);
        }

        rb.velocity = moveVector;
        if(horizontalInput < 0){
            transform.localScale = new Vector3(-1,1,1);
        } else if(horizontalInput > 0){
            transform.localScale = new Vector3(1,1,1);
        }

        if(rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        }


    }

    public void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground"))
        isJumping = false;
        animator.SetBool("isJumping" , false);
    }
}
