using System.Collections;
using UnityEngine;

public class HP_ItemsDrop : MonoBehaviour
{
    public float moveSpeed = 100f;          // tốc độ bay về player
    public GameObject healEffectPrefab;     // prefab hiệu ứng hồi máu

    private Transform player;
    private bool isMovingToPlayer = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Const.PLAYER_TAG).transform;
    }

    private void Update()
    {
        if (isMovingToPlayer)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.PLAYER_TAG))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                HealPlayer(playerHealth);
                SpawnHealEffect(player);
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag(Const.WALL_TAG))
        {
            isMovingToPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Const.WALL_TAG))
        {
            isMovingToPlayer = false;
        }
    }

    /// <summary>
    /// Hồi máu cho người chơi và phát âm thanh.
    /// </summary>
    private void HealPlayer(PlayerHealth playerHealth)
    {
        float healAmount = playerHealth.maxHealth * 0.3f;
        playerHealth.currentHealth += healAmount;
        playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth, playerHealth.maxHealth);

        playerHealth.onHealthChange?.Invoke(playerHealth.currentHealth, playerHealth.maxHealth);
        AudioController.Instance.PlaySound(AudioController.Instance.health);
    }

    /// <summary>
    /// Sinh hiệu ứng hồi máu, gắn vào Player (làm parent).
    /// </summary>
    private void SpawnHealEffect(Transform parent)
    {
        if (healEffectPrefab != null)
        {
            GameObject effect = Instantiate(
                healEffectPrefab,
                parent.position,
                Quaternion.identity,
                parent
            );
            AutoScaleDestroy auto = effect.GetComponent<AutoScaleDestroy>();
            if (auto != null)
            {
                auto.StartScale(1.5f);
            }
        }
    }
}
