using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageBeam : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damagePerSecond;

    [SerializeField] private PlayerHealth playerInZone;
    [SerializeField] private Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>(); //tìm kiếm PlayerHealth trong other 
        if (player != null) //player != null tức là other có PlayerHealth, other chính là Player hay nói cách khác other có tag là "Player"
        {
            AudioController.Instance.PlaySound(AudioController.Instance.burnPlayer);
            playerInZone = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null && player == playerInZone)
        {
            playerInZone = null;
        }
    }
    private void OnDisable()
    {
        playerInZone = null;
    }

    private void Update()
    {
        if (playerInZone != null)
        {
            float damageThisFrame = damagePerSecond * Time.deltaTime; //nhớ công thức
            playerInZone.TakeDamageFromBeam(damageThisFrame);
        }
    }
}
