using UnityEngine;
using UnityEngine.UI;

public class CoinUIManager : MonoBehaviour
{
    [Header("Menu UI")]
    [SerializeField] private Text totalCoinText;

    [Header("Gameplay UI")]
    [SerializeField] private Text coinTextGameplay;
    [SerializeField] private Text coinTextCompletedPanel;
    [SerializeField] private Text coinTextOverPanel;


    private void Start()
    {
        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        // Total coin
        if (totalCoinText != null)
            totalCoinText.text = CoinManager.Instance.totalCoinValue.ToString();

        // In-game coin
        if (coinTextGameplay != null)
            coinTextGameplay.text = CoinManager.Instance.inGameCoin.ToString();

        if (coinTextCompletedPanel != null)
            coinTextCompletedPanel.text = CoinManager.Instance.inGameCoin.ToString();

        if (coinTextOverPanel != null)
            coinTextOverPanel.text = CoinManager.Instance.inGameCoin.ToString();
        
    }
}
