using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunChanger : MonoBehaviour
{
    [SerializeField] private CanvasGroup cnvsGroup;
    [SerializeField] private Sprite gun1, gun2, gun3;
    [SerializeField] private Image icon;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.playerstats.GunChanged.AddListener(ChangeIcon);
    }

    private void Update()
    {
        if (PlayerStats.playerstats.playerCntrl.isturret)
        {
            cnvsGroup.alpha = 1;
        }
        else
        {
            cnvsGroup.alpha = 0;
        }
    }

    void ChangeIcon()
    {
        switch (PlayerStats.playerstats.gunEquipped)
        {
            case GunType.Damage:
                icon.sprite = gun1;
                break;
            case GunType.Slow:
                icon.sprite = gun2;
                break;
            case GunType.KickBack:
                icon.sprite = gun3;
                break;
        }
    }
}
