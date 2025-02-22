using UnityEngine.UI;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Text _scoreText;
    void Start()
    {
        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_scoreText != null)
        {
            _scoreText.text = enemySpawner.totalEnemiesKilled.ToString();
        }
    }
}
