using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveAlertUIManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Text waveText;
    [SerializeField] private TMP_Text waveTextMeshPro;

    private void OnEnable()
    {
        UpdateWaveText();
    }

    private void UpdateWaveText()
    {
        int waveIndex = enemySpawner.currentWaveIndex + 1;
        waveText.text = $"WAVE {waveIndex}";
        if (waveTextMeshPro != null)
        {
            waveTextMeshPro.text = $"WAVE {waveIndex}";
        }
    }

    //gọi từ Animation Event khi hiệu ứng Wave Alert kết thúc
    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
