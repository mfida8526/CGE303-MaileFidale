using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{

    //player movement speed
    public float moveSpeed = 5f;

    //force applied when jumping
    public float jumpForce = 10f;

    //layer mask for detecting ground
    public LayerMask groundLayer;    //force applied when jumping
    public Transform groundcheck;
    public float groundCheckRadius = 0.2f;

    //bool to keep track of if we are on the ground
    private bool isGrounded; 

    //reference to the Rigidbody@D component
    private Rigidbody2D rb;

    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        //get the Rigidbody2D component attached to this GameObject
        rb = GetComponent<Rigidbody2D>();

        //ensure the groundCheck variable is assinged
        if (groundcheck == null)
        {
            Debug.LogError("groundCheck not assigned to the player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //get input for horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        //check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //apply an upward force for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }


    void FixedUpdate()
    {
        //move the player using Rigidbody2D in FixedUpdate
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        //check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, groundLayer);

        //ensure the player is facing the direction of movement
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);   //facing right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);   //facing left
        }
    }
}
