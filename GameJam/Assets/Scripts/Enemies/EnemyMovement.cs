using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement enemyMovement;

    public Transform[] patrolPoints;
    public float moveSpeed;
    private float moveSpeedSave;
    public int patrolDestination;

    //CHASING
    public Transform playerTransform;  
    public bool isChasing;
    public float chaseDistance;

    //UTILIDAD
    private Transform myTransform;
    private byte zero = 0 , one = 1 ; 
    private float pointFive = .5f ;
    public float timeSlowed;
   
    private void Start()
    {
        moveSpeedSave = moveSpeed;
    }

    private void Awake()
    {
        myTransform = transform;
    }

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

    public void Slowness(float slow)
    {
        moveSpeed = moveSpeed * slow;
        StartCoroutine(TimeSlowned());
       
       
    }
    IEnumerator TimeSlowned()
    {
        yield return new WaitForSeconds(timeSlowed);
        moveSpeed = moveSpeedSave;
    }
}
