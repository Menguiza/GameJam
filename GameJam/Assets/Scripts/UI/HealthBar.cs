using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image bar;

    private float maxAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        maxAmount = PlayerStats.playerstats.PlayerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = PlayerStats.playerstats.PlayerCurrentHealth / maxAmount;
    }
}
