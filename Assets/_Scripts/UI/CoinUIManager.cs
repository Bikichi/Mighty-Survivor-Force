using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUIManager : MonoBehaviour
{
    [SerializeField] private Text coinText;

    private void Start()
    {
        UpdateCoinUI();
    }
        
    public void UpdateCoinUI()
    {
        coinText.text = $"{CoinManager.Instance.totalCoinValue}";
    }
}
