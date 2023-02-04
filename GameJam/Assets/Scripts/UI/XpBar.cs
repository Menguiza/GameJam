using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    
    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = PlayerStats.playerstats.CurrentXp / PlayerStats.playerstats.XpToReach;
    }
}
