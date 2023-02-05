using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

enum State
{
    Idle, Attack
}

public class AntController : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackRange, timeBetweenShots, force, offset;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Animator anim;
    
    private Transform playerTrans;
    private State state = State.Idle;
    private float remainingCD, life, maxLife = 100, tick;
    private Vector2 dir;
    private IEnemy _enemyImplementation;

    float Life
    {
        get => life;
        set
        {
            if (value >= 0 && value <= maxLife)
            {
                life = value;
            }
            else if (value < 0)
            {
                life = 0;
                PlayerStats.playerstats.GainXp(2);
                Destroy(gameObject);
            }
            else if (value > maxLife)
            {
                life = maxLife;
            }
        }
    }
    
    private void Awake()
    {
        life = maxLife;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = FindObjectOfType<PlayerController>().transform;
        remainingCD = timeBetweenShots;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTrans.position);

        if (distance <= attackRange && PlayerStats.playerstats.PlayerCurrentHealth > 0)
        {
            if(state != State.Attack) state = State.Attack;
        }
        else
        {
            if(state != State.Idle) state = State.Idle;
        }

        Behavior();
    }

    void Behavior()
    {
        Flip();
        
        if (state == State.Attack)
        {
            if (remainingCD > 0)
            {
                remainingCD -= Time.deltaTime;
                if (remainingCD <= 0)
                {
                    Launch();
                    remainingCD = timeBetweenShots;
                }
            }
        }
        else
        {
            remainingCD = timeBetweenShots;
        }
    }

    void Launch()
    {
        if (playerTrans.position.x > transform.position.x + offset)
        {
            CalculateDirection(0);
        }
        else if(playerTrans.position.x < transform.position.x - offset)
        {
            CalculateDirection(180);
        }
        else
        {
            return;
        }

        anim.SetTrigger("Attack");
    }

    public void Shoot()
    {
        SoundManager.soundManager.PlaySnapShot(2);
        Rigidbody2D _rb = Instantiate(projectilePrefab, launchPoint.position, quaternion.identity).GetComponent<Rigidbody2D>();
        _rb.AddForce(dir * force, ForceMode2D.Impulse);
    }

    void Flip()
    {
        if (playerTrans.position.x > transform.position.x)
        {
            transform.localScale= new Vector3(-1,1,1);
        }
        else if(playerTrans.position.x < transform.position.x)
        {
            transform.localScale= new Vector3(1,1,1);
        }
    }
    
    void CalculateDirection(float angle)
    {
        float angleRad;
        
        //Transformation to Radians
        angleRad = Mathf.Deg2Rad * angle;

        //Calculate direction
        dir.x = Mathf.Cos(angleRad);
        dir.y = Mathf.Sin(angleRad);
    }
    
    public void RecieveDamage(int damage)
    {
        Life -= damage;
        StartCoroutine(Damaged());
    }
    
    IEnumerator Damaged()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Player")) tick = 1;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (tick > 0)
            {
                tick -= Time.deltaTime;
                if (tick <= 0)
                {
                    PlayerStats.playerstats.DamagePlayer(5);
                    tick = 1;
                }
            }
        }
    }
}
