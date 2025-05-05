using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //variable to store the health of the player
    public int health = 100;

    //a reference to the health bar
    //this must be set in the inspector
    public DisplayBar healthBar;

    //reference to the Rigidbody2D of the player
    private Rigidbody2D rb;

    //knockback force when the player collides with the enemy
    public float knockbackForce = 5f;

    //a prefab to spawn when the player dies
    //this must be set in the inspector
    public GameObject playerDeathEffect;

    //bool to keep track if the player has been hit recently
    public static bool hitRecently = false;

    //time in seconds to recover from a hit
    public float hitRecoveryTime = 0.2f;

    //reference to play audio
    private AudioSource playerAudio;
    public AudioClip playerHitSound;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //set the animator reference
        animator = GetComponent<Animator>();

        //set the AudioSource reference
        playerAudio = GetComponent<AudioSource>();

        //set the Rigidbody2D reference
        rb = GetComponent<Rigidbody2D>();

        //check if the Rigidbody2D reference is null
        if(rb == null)
        {
            //log an error message if the rb is null
            Debug.LogError("Rigibody2D not found on player");
        }

        //set the healthBar max value to the player's health
        healthBar.SetMaxValue(health);

        //initialize hitRecently to false
        hitRecently = false;
    }

    //a function to knock the player back when they collide with an enemy
    public void KnockBack(Vector3 enemyPosition)
    {
        //if the player has been hit recently
        if(hitRecently)
        {
            //return out of the function
            return;
        }

        //set hitRecently to true
        hitRecently = true;

        if (gameObject.activeSelf)
        {
            //start the coroutine to reset hitRecently
            StartCoroutine(RecoverFromHit());
        }

        //calculate the direction of the knockback
        Vector2 direction = transform.position - enemyPosition;

        //normalize the direction vector
        //this gives a consistent knockback force regardless of the distance between the player and the enemy
        direction.Normalize();

        //add upward direction to the knockback
        direction.y = direction.y * 0.5f + 0.5f;

        //add force to the player in the direction of the knockback
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    //coroutine to reset hitRecently after hitRecoveryTime seconds
    IEnumerator RecoverFromHit()
    {
        //wait for hitRecoveryTime (0.2) seconds
        yield return new WaitForSeconds(hitRecoveryTime);

        //set hitRecently to false
        hitRecently = false;

        //set the hit animation to false
        animator.SetBool("hit", false); 
    }

    //a function to take damage
    public void TakeDamage(int damage)
    {
        //subtract the damage from the health
        health -= damage;

        //update the health bar
        healthBar.SetValue(health);

        //if the health is less than or equal to 0
        if(health <= 0)
        {
            //call the Die method
            Die();
        }
        else
        {
            //play the playerHitSound
            playerAudio.PlayOneShot(playerHitSound);

            //play the player hit animation
            animator.SetBool("hit", true);
        }
    }

    //a function to die
    public void Die()
    {
        //set gameover to true
        ScoreManager.gameOver = true;

        //Instantiate the death effect at the player's position
        GameObject deathEffect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

        //destroy the death effect after 2 seconds
        Destroy(deathEffect, 2f);

        //disable the player object
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
