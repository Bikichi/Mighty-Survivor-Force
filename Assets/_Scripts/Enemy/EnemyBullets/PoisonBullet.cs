using UnityEngine;

public class PoisonBullet : EnemyArcBullet
{
    [Header("Poison Settings")]
    [SerializeField] private GameObject poisonAreaPrefab; // Prefab vùng độc
    [SerializeField] private float poisonAreaDuration;

    protected override void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.WALL_TAG) || col.CompareTag(Const.PLANE_TAG))
        {
            SpawnPoisonField();
            Destroy(gameObject);
        }
        if (col.CompareTag(Const.PLAYER_TAG))
        {
            SpawnPoisonField();
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageBullet);
            Destroy(gameObject);
        }
    }

    public void SpawnPoisonField()
    {
        if (poisonAreaPrefab != null)
        {
            GameObject poison = Instantiate(poisonAreaPrefab, transform.position, Quaternion.identity);

            Destroy(poison, poisonAreaDuration);
        }
    }
}
