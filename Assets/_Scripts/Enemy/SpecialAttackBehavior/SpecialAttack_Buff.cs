using System.Collections;
using UnityEngine;

public class SpecialAttack_Buff : SpecialAttackBehavior
{
    [Header("Buff Settings")]
    [SerializeField] private float prepareDelay = 0.5f;
    [SerializeField] private float buffDuration = 5f;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float damageMultiplier = 1.5f;
    [SerializeField] private float cooldownMultiplier = 0.5f;
    [SerializeField] private string prepareAnimationBool = "Prepare";

    [Header("Material Settings")]
    [SerializeField] private Material buffMaterial;  //Material khi buff
    private Material originalMaterial;               //lưu lại material gốc
    private SkinnedMeshRenderer meshRenderer;        //renderer chính của quái

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

        anim.SetBool(prepareAnimationBool, true);

        yield return new WaitForSeconds(prepareDelay);

        anim.SetBool(prepareAnimationBool, false);

        movement.enemyMoveSpeed *= speedMultiplier;

        attack.attackDamage *= damageMultiplier;

        attack.attackCooldown *= cooldownMultiplier;

        yield return new WaitForSeconds(buffDuration);

        movement.enemyMoveSpeed /= speedMultiplier;

        attack.attackDamage /= damageMultiplier;

        attack.attackCooldown /= cooldownMultiplier;

        isBuffing = false;
    }
}
