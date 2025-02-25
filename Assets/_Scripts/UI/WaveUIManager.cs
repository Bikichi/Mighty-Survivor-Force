using UnityEngine;
using UnityEngine.UI;

public class WaveUIManager : MonoBehaviour
{
    private float _currentLevel;
    public float _pointExperience = 0;
    public float _maxLevelUpExperience;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Slider _levelBar;
    [SerializeField] private Text _levelText;

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
        if (enemySpawner != null && enemySpawner.waves.Count > enemySpawner.currentWaveCount)
        {
            _currentLevel = enemySpawner.currentWaveCount + 1;
            _pointExperience = enemySpawner.waves[enemySpawner.currentWaveCount].enemiesWaveKilled;
            _maxLevelUpExperience = enemySpawner.waves[enemySpawner.currentWaveCount].waveQuota;
        }
    }

    private void UpdateUI()
    {
        if (_levelBar != null)
        {
            _levelBar.maxValue = _maxLevelUpExperience;
            _levelBar.value = _pointExperience;
        }

        if (_levelText != null)
        {
            _levelText.text = _currentLevel.ToString();
        }
    }
}