using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//projectile class that controls the movement of the projectile
//attach this script to the projectile prefab
public class Projectile : MonoBehaviour
{
    //Rigidbody2D component of the projectile
    private Rigidbody2D rb;

    //speed of the projectile with a default value of 20
    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        //get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        //set the velocity of the projectile to fire
        //to the right at the speed
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
