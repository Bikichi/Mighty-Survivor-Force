using UnityEngine;

public class HP_ItemsDrop : MonoBehaviour
{
    public float moveSpeed = 100f; // tốc độ bay về player

    private Transform player;
    private bool isMovingToPlayer = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Const.PLAYER_TAG).transform;
    }

    private void Update()
    {
        if (isMovingToPlayer)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
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
        else if (other.CompareTag(Const.WALL_TAG))
        {
            isMovingToPlayer = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Const.WALL_TAG))
        {
            isMovingToPlayer = false;
        }
    }
}
