using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{

    //create a variable to keep track of whether the trigger zone is active
    bool active = true;

    PlatformerPlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerPlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the trigger zone is active and the player enters the trigger zone
        if(active && collision.gameObject.tag == "Player")
        {
            //deactivate the trigger zone
            active = false;

            //add 1 to the score when the player enters the trigger zone
            ScoreManager.score++;

            //play coin sound effect
            
            //sett
            //PlatformerPlayerController playerController = collision.gameObject.GetComponent<PlatformerPlayerController>().PlayCoinSound;

            playerController.PlayCoinSound();

            //destroy this game object
            Destroy(gameObject);

        }
        
    }
}
