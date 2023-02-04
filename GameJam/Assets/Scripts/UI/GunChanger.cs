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

    void ChangeIcon()
    {
        switch (PlayerStats.playerstats.gunEquipped)
        {
            case GunType.None:
                icon.sprite = null;
                cnvsGroup.alpha = 0;
                break;
            case GunType.Pochita:
                cnvsGroup.alpha = 1;
                icon.sprite = gun1;
                break;
            case GunType.Ricardo:
                cnvsGroup.alpha = 1;
                icon.sprite = gun2;
                break;
            case GunType.Gaia:
                cnvsGroup.alpha = 1;
                icon.sprite = gun3;
                break;
        }
    }
}
