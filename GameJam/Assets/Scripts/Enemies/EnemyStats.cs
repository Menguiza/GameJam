using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyMaxHealth = 100 , enemyCurrentHealth;
    public int xp;

    private void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enemyCurrentHealth -= 100;
        }

        if (enemyCurrentHealth <= 0)
        {
            PlayerStats.playerstats.GainXp(xp);
            gameObject.SetActive(false);
        }

    }

    public void DamageEnemy(int damage) 
    {
        enemyMaxHealth -= damage;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            PlayerStats.playerstats.DamagePlayer(34);
        }
    }

}
