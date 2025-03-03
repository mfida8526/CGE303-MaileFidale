using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    //creat a cariable to keep track of whether the trigger zone is active
    bool active = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the trigger zone is active and the player enters the trigger zone
        if(active && collision.gameObject.tag == "Player")
        {
            //deactivate the trigger zone
            active = false;

            //add 1 to the score when the player enters the trigger zone
            ScoreManager.score++;

            //destroy this game object
            Destroy(gameObject);

        }
        
    }
}
