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

            Destroy(gameObject);
        }
    }
}
