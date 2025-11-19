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
    private SkinnedMeshRenderer skinnedMeshRenderer;        //renderer chính của quái

    private bool isBuffing;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyAttack attack;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        attack = GetComponentInChildren<EnemyAttack>();
        anim = GetComponentInChildren<Animator>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterial = skinnedMeshRenderer.material;
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
        movement.isMovementDisabled = true;

        yield return new WaitForSeconds(prepareDelay);

        AudioController.Instance.PlaySound(AudioController.Instance.buff);
        
        anim.SetBool(prepareAnimationBool, false);
        skinnedMeshRenderer.material = buffMaterial;
        movement.isMovementDisabled = false;

        float originalSpeed = movement.enemyMoveSpeed;

        movement.enemyMoveSpeed = originalSpeed * speedMultiplier;
        attack.attackDamage *= damageMultiplier;
        attack.attackCooldown *= cooldownMultiplier;

        yield return new WaitForSeconds(buffDuration);

        skinnedMeshRenderer.material = originalMaterial;

        movement.enemyMoveSpeed = originalSpeed;
        attack.attackDamage /= damageMultiplier;
        attack.attackCooldown /= cooldownMultiplier;

        isBuffing = false;
    }

}
