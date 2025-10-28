using UnityEngine;

public class HP_ItemsDrop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.PLAYER_TAG))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                float healAmount = playerHealth.maxHealth * 0.3f;
                playerHealth.currentHealth += healAmount;

                playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth, playerHealth.maxHealth);
                playerHealth.onHealthChange?.Invoke(playerHealth.currentHealth, playerHealth.maxHealth);

                Destroy(gameObject);
            }
        }
    }
}
