using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageIndicatorUI : MonoBehaviour
{
    [SerializeField] private Image damageImage;
    [SerializeField] private float flashDuration; // tổng thời gian fade
    [SerializeField] private Color flashColor = new Color(1, 0, 0, 0.8f);
    [SerializeField] private PlayerHealth playerHealth;

    private Color originalColor;

    private void Start()
    {
        originalColor = damageImage.color;
        damageImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); //ban đầu trong suốt

        playerHealth = FindObjectOfType<PlayerHealth>();

        playerHealth.onTakeDamage.AddListener(OnPlayerTakeDamage);
    }

    private void OnPlayerTakeDamage()
    {
        StopAllCoroutines();
        StartCoroutine(FadeDamage());
    }

    private IEnumerator FadeDamage()
    {
        //set ngay màu đỏ ban đầu với alpha flashColor.a
        Color startColor = flashColor;
        damageImage.color = startColor;

        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(flashColor.a, 0, elapsed / flashDuration);
            //Mathf.Lerp dùng để tính giá trị trung gian giữa hai số với công thức result = a + (b - a) * t
            //cụ thể ở đây là giảm dần từ flashColor.a về 0f với tỉ lệ elapsed / flashDuration
            damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }

        //đảm bảo cuối cùng alpha = 0
        damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
    }

    private void OnDestroy()
    {
        playerHealth.onTakeDamage.RemoveListener(OnPlayerTakeDamage);
    }
}
