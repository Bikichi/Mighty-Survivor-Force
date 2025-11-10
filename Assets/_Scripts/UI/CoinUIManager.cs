using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUIManager : MonoBehaviour
{
    [SerializeField] private Text coinTextInGameplay;
    [SerializeField] private Text coinTextInGameCompletedPanel;


    private void Start()
    {
        UpdateCoinUI();
    }
        
    public void UpdateCoinUI()
    {
        if (coinTextInGameplay != null)
            coinTextInGameplay.text = $"{CoinManager.Instance.inGameCoin}";

        //Update ngay cả khi gameobject chứa coinTextInGameCompletedPanel không enable
        if (coinTextInGameCompletedPanel != null)
        {
            coinTextInGameCompletedPanel.text = $"{CoinManager.Instance.totalCoinValue}";
        }
    }
}
