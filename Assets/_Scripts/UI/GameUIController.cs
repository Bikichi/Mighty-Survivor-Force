using UnityEngine;

public class GameController : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public UIManager uiManager;

    void Start()
    {
        enemySpawner.onWaveCompleted.AddListener(uiManager.ShowSkillPanel);
    }
}
