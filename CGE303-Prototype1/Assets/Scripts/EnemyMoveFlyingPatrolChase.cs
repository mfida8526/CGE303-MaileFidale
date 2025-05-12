using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFlyingPatrolChase : MonoBehaviour
{

    //public references for patrol points
    public GameObject[] patrolPoints;

    //public variables for movement
    public float speed = 2f;
    public float chaseRange = 3f;

    //enemy state enum
    public enum EnemyState { Patrolling, Chasing };

    //current enemy state
    public EnemyState currentState = EnemyState.Patrolling;
    public GameObject target;
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    //current patrol point index
    private int currentPatrolPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //find player transform
        player = GameObject.FindWithTag("Player");

        //get Rigidbody2D component of the enemy
        rb = GetComponent<Rigidbody2D>();

        //get the SpriteRenderer component of the enemy
        sr = GetComponent<SpriteRenderer>();

        //check if patrol points are assigned
        if(patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned!");
        }

        //set initial target to first patrol point
        target = patrolPoints[currentPatrolPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //update state based on player and target distance
        UpdateState();

        //move and face based on current state
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
        }

        //you can use Debug.DrawLine to draw a line between two points in the Scene view
        Debug.DrawLine(transform.position, target.transform.position, Color.red);
    }

    //update enemy state based on player proximity
    void UpdateState()
    {
        if(IsPlayerInChaseRange() && currentState == EnemyState.Patrolling)
        {
            currentState = EnemyState.Chasing;
        }
        else if(!IsPlayerInChaseRange() && currentState == EnemyState.Chasing)
        {
            currentState = EnemyState.Patrolling;
        }
    }

    bool IsPlayerInChaseRange()
    {
        if(player == null)
        {
            Debug.LogError("Player not found");
                return false;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance <= chaseRange;
    }

    void Patrol()
    {
        //check if reached current target
        if(Vector2.Distance(transform.position, target.transform.position) <= 0.5f)
        {
            //update target to next patrol point (wrap around)
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        //set target to current patrol point
        target = patrolPoints[currentPatrolPointIndex];

        MoveTowardsTarget();
    }

    void ChasePlayer()
    {
        target = player;
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        //calculate direction towards target
        Vector2 direction = target.transform.position - transform.position;

        //normalize direction
        direction.Normalize();

        //move towards target with rb
        rb.velocity = direction * speed;

        //face forward
        FaceForward(direction);
    }

    private void FaceForward(Vector2 direction)
    {
        if(direction.x < 0)
        {
            sr.flipX = false;
        }
        else if(direction.x > 0)
        {
            sr.flipX = true;
        }
    }

    //draw circles for patrol points in the Scene view
    private void OnDrawGizmos()
    {
       if(patrolPoints != null)
        {
            Gizmos.color = Color.green;
            foreach(GameObject point in patrolPoints)
            {
                Gizmos.DrawWireSphere(point.transform.position, 0.5f);
            }
        }
    }
}
