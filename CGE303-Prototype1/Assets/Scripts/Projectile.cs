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

    //damage of the projectile with a default value of 20
    public int damage = 20;

    //impact effect of the projectile
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        //get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        //set the velocity of the projectile to fire
        //to the right at the speed
        rb.velocity = transform.right * speed;
    }

    //function called when the projectile collides with another object
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //get the Enemy component of the object that was hit
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        //if the object that was hit has an Enemy component
        if(enemy != null)
        {
            //call the TakeDamage function of the Enemy component
            enemy.TakeDamage(damage);
        }

        //if the object that was hit is not the player
        if(hitInfo.gameObject.tag != "Player")
        {
            //instantiate the impact effect
            Instantiate(impactEffect, transform.position, Quaternion.identity);

            //destroy the projectile
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
