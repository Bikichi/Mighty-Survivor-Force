using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public LivingEntity livingEntity;
    public Image healthValue;

    private void Start()
    {
        livingEntity.onHealthChange.AddListener(UpdateHP);
    }

    private void UpdateHP(float healthpoint, float maxhealth)
    {
        healthValue.fillAmount = healthpoint / maxhealth;
    }
}