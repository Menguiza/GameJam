using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

public enum GunType
{
    Damage, Slow, KickBack
}

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerstats;
    
    [SerializeField] private float playerMaxHealth = 100, firstXpToReach = 10;

    public UnityEvent GunChanged;
    public PlayerController playerCntrl { get; private set; }
    
    private float playerCurrentHealth, xpToReach, currentXp = 0;

    #region Accessors
    
    public GunType gunEquipped { get; private set; }

    public float PlayerMaxHealth
    {
        get => playerMaxHealth;
    }
    
    public float XpToReach
    {
        get => xpToReach;
    }
    
    public float PlayerCurrentHealth
    {
        get => playerCurrentHealth;
        set
        {
            if (value >= 0 && value <= playerMaxHealth)
            {
                playerCurrentHealth = value;
            }
            else if(value < 0)
            {
                playerCurrentHealth = 0;
            }
            else
            {
                playerCurrentHealth = playerMaxHealth;
            }
        }
    }
    
    public float CurrentXp
    {
        get => currentXp;
        set
        {
            if (value >= 0 && value < xpToReach)
            {
                currentXp = value;
            }
            else if (value >= xpToReach)
            {
                currentXp = 0;
                xpToReach += (xpToReach * 0.2f);
            }
        }
    }

    #endregion
    
    private void Awake()
    {
        playerstats = this;
        playerCurrentHealth = playerMaxHealth;
        xpToReach = firstXpToReach;
        gunEquipped = GunType.Damage;
        playerCntrl = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        ChangeGun();
    }

    public void DamagePlayer(int damage)
    {
        PlayerCurrentHealth -= damage;
        StartCoroutine(Damaged());
    }

    public void HealPlayer(int health)
    {
        playerCurrentHealth += health;
    }

    public void GainXp(int xp)
    {
        CurrentXp += xp;
    }

    public void ChangeGun()
    {
        if (Input.GetButtonDown("Dash") && playerCntrl.isturret)
        {
            gunEquipped++;

            if ((int)gunEquipped > 2)
            {
                gunEquipped = 0;
            }
            
            GunChanged.Invoke();
        }
    }

    IEnumerator Damaged()
    {
        playerCntrl.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        playerCntrl.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
