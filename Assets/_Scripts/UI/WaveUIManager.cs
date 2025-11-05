using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveUIManager : MonoBehaviour
{
    private float _currentLevel;
    public float _pointExperience = 0;
    public float _maxLevelUpExperience;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Slider _levelBar;
    [SerializeField] private TMP_Text _levelTextMP;

    private void Start()
    {
        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        UpdateLevelData();
        UpdateUI();
    }

    private void Update()
    {
        UpdateLevelData();
        UpdateUI();
    }

    private void UpdateLevelData()
    {
        if (enemySpawner == null) return;

        var waves = enemySpawner.waves;
        int currentWaveIndex = enemySpawner.currentWaveIndex;

        if (currentWaveIndex >= 0 && currentWaveIndex < waves.Count)
        {
            WaveData currentWave = waves[currentWaveIndex];

            _currentLevel = currentWaveIndex + 1;
            _pointExperience = currentWave.enemiesKilled;
            _maxLevelUpExperience = currentWave.TotalQuota();  //tổng số quái cần tiêu diệt để hoàn thành wave
        }
    }



    private void UpdateUI()
    {

        _levelBar.maxValue = _maxLevelUpExperience;
        _levelBar.value = _pointExperience;

        _levelTextMP.text = _currentLevel.ToString();

    }
}