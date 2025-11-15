using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage = 20f;                
    public float radius = 5f;               
    public LayerMask targetMask;             

    private void Start()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.bossExplode);
        DealDamage();
    }

    private void DealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, targetMask);

        foreach (Collider col in hits)
        {
            PlayerHealth ph = col.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
