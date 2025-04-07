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
    public bool isGrounded; 

    //reference to the Rigidbody@D component
    private Rigidbody2D rb;

    private float horizontalInput;

    public AudioClip jumpSound;
    private AudioSource playerAudio;

    public AudioClip coinSound;
    //private AudioSource coinAudio;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        //get the Rigidbody2D component attached to this GameObject
        rb = GetComponent<Rigidbody2D>();

        //set the reference for the AudioSource
        playerAudio = GetComponent<AudioSource>();

        //set the reference for the Animator
        animator = GetComponent<Animator>();

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

            //play jump sound effect
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }


    void FixedUpdate()
    {
        //move the player using Rigidbody2D in FixedUpdate
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        //set animator parameter xVelocityAbs to absolute ovalue of x velocity
        animator.SetFloat("xvelocityAbs", Mathf.Abs(rb.velocity.x));

        //set animator parameter yVelocity to y velocity
        animator.SetFloat("yVelocity", rb.velocity.y);

        //check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, groundLayer);

        animator.SetBool("onGround", isGrounded);

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

    public void PlayCoinSound()
    {
        //play coin sound
        playerAudio.PlayOneShot(coinSound, 1.0f);
    }
}
