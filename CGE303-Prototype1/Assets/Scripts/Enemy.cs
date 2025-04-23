using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //enemy's health
    public int health = 100;

    //a prefab to spawn when the enemy dies
    public GameObject deathEffect;

    //a method to take damage
    public void TakeDamage(int damage)
    {
        //subtract the damage dealt from the health
        health -= damage;
        
        //if the health is less than or equal to 0
        if (health <= 0)
        {
            //call the Die function
            Die();
        }
    }

    void Die()
    {
        //spawn a death effect
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        //destroy the enemy
        Destroy(gameObject);
    }
    
}
