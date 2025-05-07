using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//require a RigidBody2D and an Animator on the enemy
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class EnemyMoveWalkingChase : MonoBehaviour
{
    //range at which the enemy will chase the player
    public float chaseRange = 4f;

    //speed of the enemy movement
    public float enemyMovementSpeed = 1.5f;

    //transform of the player object
    private Transform playerTransform;

    //Rigidbody2D component of the enemy
    private Rigidbody2D rb;

    //animator component of the enemy
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //get the Rigidbody2D component of the enemy
        rb = GetComponent<Rigidbody2D>();

        //get the Animator component of the enemy
        anim = GetComponent<Animator>();

        //get the player transform using the "Player" tag
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //this Vector2 is a 2D arrow from the enemy to the player
        Vector2 playerDirection = playerTransform.position - transform.position;

        //distance between the enemy and the player
        //the magnitude of the vector is the distance without the direction
        float distanceToPlayer = playerDirection.magnitude;

        //check if the player is within chase range
        if (distanceToPlayer <= chaseRange)
        {
            //we need the direction to the player on only the x axis

            //normalize gives us the direction to the player without the distance
            playerDirection.Normalize();

            //setting the y axis to 0 because we only want to move on the x axis
            playerDirection.y = 0f;

            //rotate the enemy to face the player
            FacePlayer(playerDirection);

            //if there is ground ahead of the enemy
            if (IsGroundAhead())
            {
                MoveTowardsPlayer(playerDirection);
            }
            //if there is no ground ahead, stop moving
            else
            {
                StopMoving();
            }
        }
        else
        {
            StopMoving();
        }
    }

    //bool function to check if there is ground in front of the enemy to walk on
    bool IsGroundAhead()
    {
        //ground check variables
        float groundCheckDistance = 2.0f; //adjust this distance as needed
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        //determine which direction the enemy is facing
        Vector2 enemyFacingDirection = transform.rotation.y == 0 ? Vector2.left : Vector2.right;

        //raycast to check for ground ahead of the enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down + enemyFacingDirection, groundCheckDistance, groundLayer);

        //return true if ground is detected
        return hit.collider != null;
    }

    private void FacePlayer(Vector2 playerDirection)
    {
        if(playerDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void MoveTowardsPlayer(Vector2 playerDirection)
    {
        //move the enemy towards the player by setting the velocity
        //to a new Vector2 without changing the y axis of velocity
        rb.velocity = new Vector2(playerDirection.x * enemyMovementSpeed, rb.velocity.y);

        //set the animator parameter to move
        anim.SetBool("isMoving", true);
    }

    private void StopMoving()
    {
        //stop moving if the player is out of range
        rb.velocity = new Vector2(0, rb.velocity.y);

        //set the animator parameter to stop moving
        anim.SetBool("isMoving", false);
    }
}
