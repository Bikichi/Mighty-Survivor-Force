using System.Collections;
using UnityEngine;

public class SpecialAttack_Dash : SpecialAttackBehavior
{
    [Header("Dash Settings")]
    [SerializeField] private float prepareDelay = 1f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private string prepareAnimationBool = "Prepare";
    [SerializeField] private string dashAnimationBool = "Dash";
    
    [Header("Damage Settings")]
    [SerializeField] private GameObject damageArea;

    private bool isAttacking;
    public bool IsAttacking => isAttacking;
    private Transform self;
    [SerializeField] private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        self = transform;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    public override void ExecuteSpecialAttack(Transform target)
    {
        if (!isAttacking)
            StartCoroutine(DashRoutine(target));
    }

    private IEnumerator DashRoutine(Transform target)
    {
        isAttacking = true;

        EnemyMovement move = GetComponent<EnemyMovement>();
        float originalLerpSpeed = move.lerpSpeed;
        // Tắt movement trong lúc chuẩn bị dash
        move.isMovementDisabled = true;

        anim.SetBool(prepareAnimationBool, true);

        yield return new WaitForSeconds(prepareDelay);

        // Bắt đầu dash
        AudioController.Instance.PlaySoundMultipleTimes(AudioController.Instance.dash, 3, dashDuration / 3);
        anim.SetBool(prepareAnimationBool, false);
        anim.SetBool(dashAnimationBool, true);

        move.lerpSpeed = 0f;

        Vector3 direction = (target.position - transform.position).normalized;

        damageArea.SetActive(true);

        float timer = 0f;
        while (timer < dashDuration)
        {
            if (GetComponent<EnemyHealth>().IsDead)
                yield break;

            rb.velocity = direction * dashSpeed;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        damageArea.SetActive(false);

        move.lerpSpeed = originalLerpSpeed;
        move.isMovementDisabled = false;

        anim.SetBool(dashAnimationBool, false);
        isAttacking = false;
    }

}
