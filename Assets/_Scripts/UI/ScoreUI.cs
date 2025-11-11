using UnityEngine.UI;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Text _scoreTextInGameplay;
    [SerializeField] private Text _scoreTextInGameCompletedPanel;
    [SerializeField] private Text _scoreTextInGameOverPanel;
    void Start()
    {
        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
        UpdateScoreUI();
    }

    //Update khi quái vật chết
    public void UpdateScoreUI()
    {
        //Update ngay cả khi gameobject chứa _scoreTextInGameCompletedPanel không enable
        _scoreTextInGameplay.text = enemySpawner.totalEnemiesKilled.ToString();
        _scoreTextInGameCompletedPanel.text = enemySpawner.totalEnemiesKilled.ToString();
        _scoreTextInGameOverPanel.text = enemySpawner.totalEnemiesKilled.ToString();
    }
}
