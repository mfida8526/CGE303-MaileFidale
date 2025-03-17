using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{

    //adjust this value in the inspector to set the player's movement speed
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    private Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        //get the Rigidbody2D component attached to the GameObject
        rb = GetComponent < Rigidbody2D > ();
    }

    // Update is called once per frame
    void Update()
    {
        //get input values for horizontal and vertical movement
        //use GetAxisRaw so the input is either 1, 0, or -1
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //normalize the movement vector to prevent faster diagonal movement
        movement.Normalize();
    }


    void FixedUpdate()
    {
        //move the player using Rigidbody2D in FixedUpdate
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }
}
