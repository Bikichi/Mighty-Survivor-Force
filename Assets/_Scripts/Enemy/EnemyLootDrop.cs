using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private GameObject coinPrefab;
    [Range(0f, 1f)]
    [SerializeField] private float coinDropChance = 0.5f; // 50% rơi coin

    [Header("Health Drop Settings")]
    [SerializeField] private GameObject healthPrefab;
    [Range(0f, 1f)]
    [SerializeField] private float healthDropChance = 0.2f; // 20% rơi máu

    [Header("Boss Drop Settings")]
    [SerializeField] private int minCoins = 5;
    [SerializeField] private int maxCoins = 10;
    [SerializeField] private Vector3 areaCenter = new Vector3(0.02f, 1.58f, -2.5f);
    [SerializeField] private Vector3 areaSize = new Vector3(10f, 0f, 10f);

    public void DropNormalLoot(Vector3 position, Quaternion rotation)
    {
        // 🪙 Coin drop
        if (coinPrefab != null && Random.value <= coinDropChance)
        {
            Instantiate(coinPrefab, position, rotation);
        }

        // ❤️ Health drop
        if (healthPrefab != null && Random.value <= healthDropChance)
        {
            Instantiate(healthPrefab, position, rotation);
        }
    }

    public void DropBossLoot(Vector3 position, Quaternion rotation)
    {
        int coinCount = Random.Range(minCoins, maxCoins + 1);

        for (int i = 0; i < coinCount; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f),
                Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
            );

            Vector3 dropPosition = position + areaCenter + randomOffset;
            Instantiate(coinPrefab, dropPosition, rotation);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.84f, 0f, 0.25f);
        Gizmos.DrawCube(transform.position + areaCenter, areaSize);
    }
#endif
}
