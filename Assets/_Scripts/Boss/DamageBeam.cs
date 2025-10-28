using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageBeam : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damagePerSecond = 20f;

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
            playerInZone.TakeDamageFromBeam(damagePerSecond * Time.deltaTime);
        }
    }
}
