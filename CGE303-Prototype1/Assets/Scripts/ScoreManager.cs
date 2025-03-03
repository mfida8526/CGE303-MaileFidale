using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//add this to work with TextMeshPro
using TMPro;
//add this to work with SceneManager to load or reload scenes
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{

    //public static variables
    //notice public static variables
    //can be accessed from any script
    //but cannot be seen in the inspector
    public static bool gameOver;
    public static bool won;
    public static int score;

    //reference to our textbox
    //this needs to be set in the inspector
    public TMP_Text textbox;

    public int ScoreToWin;


    // Start is called before the first frame update
    void Start()
    {
        //set the initial values for variables
        gameOver = false;
        won = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            textbox.text = "Score:" + score;
        }

        if (score >= ScoreToWin)
        {
            won = true;
            gameOver = true;
        }

        if (gameOver)
        {
            if (won)
            {
                textbox.text = "You win! Press R to Try Again!";
            }
            else
            {
                textbox.text = "You lose! Press R to Try Again!";
            }
            //if the game is over and they press R key on keyboard
            if (Input.GetKeyDown(KeyCode.R))
            {
                //reload the current scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }
}
