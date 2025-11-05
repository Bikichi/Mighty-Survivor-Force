using UnityEngine;

public class SplitEnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject childEnemyPrefab;
    [SerializeField] private float spawnOffset = 2f; // khoảng cách 2 bên
    [SerializeField] private EnemySpawner enemySpawner;

    private void Start()
    {
        //mỗi khi split enemy được spawn ra thì enemiesAlive phải cộng tổng thêm 3 
        //vì bản thân con quái to và 2 con quái con sẽ sinh ra khi quái to chết
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        
        enemySpawner.enemiesAlive += 2; //cộng số quái vật đang còn sống thêm 2 tức tổng 3 vì enemySpawner đã cộng 1 trước đấy khi spawn quái to
        enemySpawner.waves[enemySpawner.currentWaveIndex].AddExtraQuota(2);// cộng 2 vào tổng số quái vật ở wave này
    }

    public void SpawnChildren(Vector3 parentPosition, Quaternion parentRotation)
    {
        if (childEnemyPrefab == null)
        {
            return;
        }
        Vector3 rightOffset = parentRotation * Vector3.right * spawnOffset;
        Vector3 leftOffset = parentRotation * Vector3.left * spawnOffset;

        Instantiate(childEnemyPrefab, parentPosition + rightOffset, parentRotation);
        Instantiate(childEnemyPrefab, parentPosition + leftOffset, parentRotation);
        enemySpawner.waves[enemySpawner.currentWaveIndex].totalSpawned += 2; //tăng tổng số quái vật đã spawn ở wave này thêm 2
    }
}
