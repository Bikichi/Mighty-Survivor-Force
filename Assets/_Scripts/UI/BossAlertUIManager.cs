using UnityEngine;

public class BossAlertUIManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    public void ActivateBossUIForCurrentWave()
    {
        int currentWaveIndex = enemySpawner.currentWaveIndex;
        var waves = enemySpawner.GetAllWaves();

        if (currentWaveIndex >= waves.Count) return;

        WaveData currentWave = waves[currentWaveIndex];

        if (currentWave.bossAlertUI != null)
        {
            currentWave.bossAlertUI.SetActive(true);
        }
    }
}
