using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterDelay : MonoBehaviour
{

    //the delay before the game object is destroyed
    public float delay = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        //destroy the game object after the delay number of seconds
        Destroy(gameObject, delay);
    }

}
