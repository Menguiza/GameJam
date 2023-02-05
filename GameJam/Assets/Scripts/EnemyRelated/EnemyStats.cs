using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour,IEnemy
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
        if (currentEnemyHealth <=0)
        {
            PlayerStats.playerstats.GainXp(xp);
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            PlayerStats.playerstats.DamagePlayer(34);
            SoundManager.soundManager.PlaySnapShot(3);
        }
    }


    public void RecieveDamage(int damage)
    {
        currentEnemyHealth -= damage;
    }
}
