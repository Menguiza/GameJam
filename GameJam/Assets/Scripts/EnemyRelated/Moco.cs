using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moco : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (col.collider.CompareTag("Player"))
        {
            PlayerStats.playerstats.DamagePlayer(10);
            Destroy(gameObject);
        }
    }
}
