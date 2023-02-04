using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    //CHASING
    public Transform playerTransform; // 
    public bool isChasing;
    public float chaseDistance;

    //UTILIDAD
    private Transform myTransform;
    private byte zero = 0 , one = 1 ; 
    private float pointFive = .5f ;

    private void Awake()
    {
        myTransform = transform;
    }

    // Update is called once per frame 
    void Update()
    {
        if (isChasing)
        {
            if (myTransform.position.x > playerTransform.position.x)
            {
                myTransform.localScale = new Vector3(one, one, one);
                myTransform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (myTransform.position.x < playerTransform.position.x)
            {
                myTransform.localScale = new Vector3(-one, one, one);
                myTransform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }


        }

        else
        {
            if (Vector2.Distance(myTransform.position, playerTransform.position) < chaseDistance)

            {
                isChasing = true;

            }
       




         if (patrolDestination == zero)
        {
            myTransform.position = Vector2.MoveTowards(myTransform.position, patrolPoints[zero].position, moveSpeed * Time.deltaTime); ;

            if (Vector2.Distance(myTransform.position, patrolPoints[zero].position) < pointFive)
            {
                myTransform.localScale = new Vector3(one, one, one);
                patrolDestination = one;
            }
        }

        if (patrolDestination == one)
        {
            myTransform.position = Vector2.MoveTowards(myTransform.position, patrolPoints[one].position, moveSpeed * Time.deltaTime); ;

            if (Vector2.Distance(myTransform.position, patrolPoints[one].position) < pointFive)
            {
                myTransform.localScale = new Vector3(-one, one, one);
                patrolDestination = zero;
            }
        }
        }
    }
}
