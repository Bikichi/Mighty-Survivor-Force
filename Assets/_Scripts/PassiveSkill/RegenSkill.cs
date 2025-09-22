using UnityEngine;
using System.Collections;

public class RegenSkill : MonoBehaviour
{
    [Header("Regen Settings")]
    public float healAmount = 5f;   // hồi bao nhiêu máu
    public float interval = 2f;     // khoảng thời gian giữa các lần hồi

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

            var stats = PlayerStats.Instance;
            stats.maxHP = Mathf.Min(stats.maxHP + healAmount, stats.baseHP);

            Debug.Log($"[RegenSkill] +{healAmount} máu → HP hiện tại = {stats.maxHP}");
        }
    }
}