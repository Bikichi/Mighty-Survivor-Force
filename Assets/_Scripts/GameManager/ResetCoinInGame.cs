using UnityEngine;

public class ResetInGameCoin : MonoBehaviour
{
    private void Awake()
    {
        CoinManager.Instance.inGameCoin = 0;
    }
}