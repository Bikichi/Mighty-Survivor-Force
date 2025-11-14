using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsDrop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Const.PLAYER_TAG))
        {
            CoinManager.Instance.totalCoinValue += 1;
            CoinManager.Instance.SaveCoinValue();

            CoinManager.Instance.inGameCoin += 1;

            CoinUIManager coinUI = FindObjectOfType<CoinUIManager>();
            coinUI.UpdateCoinUI();

            AudioController.Instance.PlaySound(AudioController.Instance.collectCoin);

            Destroy(gameObject);
        }
    }
}
