using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealthEnemy = 100, currentEnemyHealth;
    public int xp;


    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxHealthEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentEnemyHealth -= maxHealthEnemy;
        }


        if (currentEnemyHealth <=0)
        {
            
            PlayerStats.playerstats.GainXp(xp);
            gameObject.SetActive(false);

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerStats player = other.gameObject.GetComponent<PlayerStats>();


        if (player != null)
        {
            PlayerStats.playerstats.DamagePlayer(34);


        }
    }

    public void DamageEnemy(int damage)
    {
        currentEnemyHealth -= damage;

    }


}
