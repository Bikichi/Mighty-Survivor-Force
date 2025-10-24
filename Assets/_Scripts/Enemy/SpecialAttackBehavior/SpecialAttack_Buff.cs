using System.Collections;
using UnityEngine;

public class SpecialAttack_Buff : SpecialAttackBehavior
{
    [Header("Buff Settings")]
    [SerializeField] private float prepareDelay = 0.5f;
    [SerializeField] private float buffDuration = 5f;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float damageMultiplier = 1.5f;
    [SerializeField] private ParticleSystem buffEffect;
    [SerializeField] private string prepareAnimationTrigger = "Prepare";

    private bool isBuffing;
    private EnemyMovement movement;
    private EnemyAttack attack;
    private Animator anim;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();
        anim = GetComponentInChildren<Animator>();
    }

    public override void ExecuteSpecialAttack(Transform target)
    {
        if (!isBuffing)
            StartCoroutine(BuffRoutine());
    }

    private IEnumerator BuffRoutine()
    {
        isBuffing = true;

        anim.SetTrigger(prepareAnimationTrigger);

        yield return new WaitForSeconds(prepareDelay);

        if (buffEffect != null)
            buffEffect.Play();

        if (movement != null)
            movement.enemyMoveSpeed *= speedMultiplier;

        if (attack != null)
            attack.attackDamage *= damageMultiplier;

        yield return new WaitForSeconds(buffDuration);

        if (movement != null)
            movement.enemyMoveSpeed /= speedMultiplier;

        if (attack != null)
            attack.attackDamage /= damageMultiplier;

        isBuffing = false;
    }
}
