using UnityEngine;
using System.Collections;

public class RegenSkill : MonoBehaviour
{
    public float healPercent = 10f;   //hồi bao nhiêu % máu tối đa mỗi tick
    public float interval = 2f;      //khoảng thời gian giữa các lần hồi

    private bool isRunning;

    private void OnEnable()
    {
        if (!isRunning) StartCoroutine(RegenLoop());
    }

    private IEnumerator RegenLoop()
    {
        isRunning = true;
        var playerHealth = FindObjectOfType<PlayerHealth>();

        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                //tính lượng máu hồi theo %
                float healAmount = playerHealth.maxHealth * (healPercent / 100f);

                playerHealth.currentHealth = Mathf.Min(
                    playerHealth.currentHealth + healAmount,
                    playerHealth.maxHealth
                );

                playerHealth.onHealthChange?.Invoke(playerHealth.currentHealth, playerHealth.maxHealth);

                Debug.Log($"[RegenSkill] +{healAmount} máu ({healPercent}% maxHP) → HP hiện tại = {playerHealth.currentHealth}");
            }
        }
    }
}
