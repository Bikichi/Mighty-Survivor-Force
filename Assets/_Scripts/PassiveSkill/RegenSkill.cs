using UnityEngine;
using System.Collections;

public class RegenSkill : MonoBehaviour
{
    [Header("Regen Settings")]
    public float healAmount = 10f;   // hồi bao nhiêu máu
    public float interval = 3f;     // khoảng thời gian giữa các lần hồi

    private bool isRunning;

    private void OnEnable()
    {
        if (!isRunning) StartCoroutine(RegenLoop());
    }

    private IEnumerator RegenLoop()
    {
        isRunning = true;
        while (true)
        {
            yield return new WaitForSeconds(interval);

            var playerHealth = FindObjectOfType<PlayerHealth>();

            playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healAmount, playerHealth.maxHealth);

            playerHealth.onHealthChange?.Invoke(playerHealth.currentHealth, playerHealth.maxHealth);

            Debug.Log($"[RegenSkill] +{healAmount} máu → HP hiện tại = {playerHealth.currentHealth}");
        }
    }
}