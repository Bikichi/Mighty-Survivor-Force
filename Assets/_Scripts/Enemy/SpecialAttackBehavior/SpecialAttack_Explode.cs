using System.Collections;
using UnityEngine;

public class SpecialAttack_Explode : SpecialAttackBehavior
{
    [Header("Explosion Settings")]
    [SerializeField] private float prepareDelay = 1.5f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionDamage = 30f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private string prepareAnimationTrigger = "Prepare";

    private bool hasExploded;
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

        anim.SetTrigger(prepareAnimationTrigger);

        yield return new WaitForSeconds(prepareDelay);

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            LivingEntity entity = hit.GetComponent<LivingEntity>();
            if (entity != null)
                entity.TakeDamage(explosionDamage);
        }

        Destroy(gameObject);
    }
}
