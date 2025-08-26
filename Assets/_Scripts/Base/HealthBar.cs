using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public LivingEntity livingEntity;
    public Image healthValue;
    [SerializeField] private TextMeshProUGUI _HPText;

    private void Start()
    {
        livingEntity.onHealthChange.AddListener(UpdateHP);
    }

    private void UpdateHP(float healthpoint, float maxhealth)
    {
        healthValue.fillAmount = healthpoint / maxhealth;
    }
    void Update()
    {
        if (_HPText != null) 
        {
            _HPText.text = livingEntity.currentHealth.ToString();
        }
    }
}