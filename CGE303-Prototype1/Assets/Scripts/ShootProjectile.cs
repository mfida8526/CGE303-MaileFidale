using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this script to the player game object
//this script will allow the player to shoot projectiles
public class ShootProjectile : MonoBehaviour
{

    //reference to the projectile prefab
    public GameObject projectilePrefab;

    //reference to the firepoint transform
    //this is where the projectile will be instantiated
    //make an empty child object of the player and
    //position it where you wantt hte projectile to be fired from
    //and then assign it to this variable in the inspector
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the player presses the fire button, call the shoot function
        if(Input.GetButtonDown("Fire1"))
        {
            //call the shoot function
            Shoot();
        }
    }

    void Shoot()
    {
        //instantiate the projectile at the firepoint position and rotation
        //and store the reference to the instantiated projectile in a variable
        GameObject firedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        //destroy the projectile after 3 seconds
        Destroy(firedProjectile, 3f);
    }
}
