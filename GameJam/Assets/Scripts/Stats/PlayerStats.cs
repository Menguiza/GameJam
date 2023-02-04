using System;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

public enum GunType
{
    None, Pochita, Ricardo, Gaia
}

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerstats;
    
    [SerializeField] private float playerMaxHealth = 100, firstXpToReach = 10;

    public UnityEvent GunChanged;
    
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
        gunEquipped = GunType.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DamagePlayer(10);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            HealPlayer(10);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GainXp(2);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            gunEquipped++;

            if ((int)gunEquipped > 3)
            {
                gunEquipped = 0;
            }
            
            GunChanged.Invoke();
        }
    }

    public void DamagePlayer(int damage)
    {
        PlayerCurrentHealth -= damage;
    }

    public void HealPlayer(int health)
    {
        playerCurrentHealth += health;
    }

    public void GainXp(int xp)
    {
        CurrentXp += xp;
    }

    public void ChangeGun(GunType type)
    {
        gunEquipped = type;
        
        Debug.Log(gunEquipped);
        
        GunChanged.Invoke();
    }
}
