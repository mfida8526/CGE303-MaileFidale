using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //enemy's health
    public int health = 100;

    //a prefab to spawn when the enemy dies
    public GameObject deathEffect;

    //a reference to the healthbar
    private DisplayBar healthBar;

    private void Start()
    {
        //find the health bar in the children of the Enemy
        healthBar = GetComponentInChildren<DisplayBar>();

        if (healthBar == null)
        {
            //if the healthbar is not found, log an error
            Debug.LogError("HealthBar (DisplayBar script) not found");
            return;
        }

        // set the max value of the health bar to the enemy's health
        healthBar.SetMaxValue(health);
    }

    //a method to take damage
    public void TakeDamage(int damage)
    {
        //subtract the damage dealt from the health
        health -= damage;

        //update the health bar
        healthBar.SetValue(health);
        
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
