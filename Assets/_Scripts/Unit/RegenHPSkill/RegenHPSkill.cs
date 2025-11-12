using UnityEngine;
using System.Collections;

public class RegenHPSkill : MonoBehaviour
{
    public float healPercent;   // hồi bao nhiêu % máu tối đa mỗi tick
    public float interval;       // khoảng thời gian giữa các lần hồi

    private Coroutine regenCoroutine;

    private void OnEnable()
    {
        if (regenCoroutine == null)
            regenCoroutine = StartCoroutine(RegenLoop());
    }

    private void OnDisable()
    {
        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
            regenCoroutine = null;
        }
    }

    private IEnumerator RegenLoop()
    {
        var playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth == null) yield break;

        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (playerHealth.currentHealth > 0f && playerHealth.currentHealth < playerHealth.maxHealth)
            {
                AudioController.Instance.PlaySound(AudioController.Instance.regenSkillTick);
                float healAmount = playerHealth.maxHealth * (healPercent / 100f);
                playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healAmount, playerHealth.maxHealth);

                playerHealth.onHealthChange?.Invoke(playerHealth.currentHealth, playerHealth.maxHealth);
            }
        }
    }
}
