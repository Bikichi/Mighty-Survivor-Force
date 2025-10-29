using System.Collections;
using UnityEngine;

public class SpecialAttack_Explode : SpecialAttackBehavior
{
    [Header("Explosion Settings")]
    [SerializeField] private float prepareDelay = 1.5f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionDamage = 30f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject explosionArea;
    [SerializeField] private string prepareAnimationBool = "Prepare";

    private bool hasExploded;
    public bool HasExploded => hasExploded;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public override void ExecuteSpecialAttack(Transform target)
    {
        if (!hasExploded)
            StartCoroutine(ExplosionRoutine());
    }

    private IEnumerator ExplosionRoutine()
    {
        hasExploded = true;

        anim.SetBool(prepareAnimationBool, true);
        explosionArea.SetActive(true);
        GetComponent<EnemyMovement>().enemyMoveSpeed = 0f;
        GetComponent<EnemyMovement>().lerpSpeed = 0f;

        yield return new WaitForSeconds(prepareDelay);

        anim.SetBool(prepareAnimationBool, false);

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.TakeDamage(explosionDamage);
            }
        }
        explosionArea.SetActive(false);
        Destroy(gameObject);

        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
