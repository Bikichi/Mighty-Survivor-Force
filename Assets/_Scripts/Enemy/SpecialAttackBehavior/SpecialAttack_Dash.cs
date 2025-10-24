using System.Collections;
using UnityEngine;

public class SpecialAttack_Dash : SpecialAttackBehavior
{
    [Header("Dash Settings")]
    [SerializeField] private float prepareDelay = 1f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private string prepareAnimationBool = "Prepare";

    private bool isAttacking;
    private Transform self;
    private Rigidbody rb;
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

        anim.SetBool(prepareAnimationBool, true);

        yield return new WaitForSeconds(prepareDelay);

        anim.SetBool(prepareAnimationBool, false);

        Vector3 direction = (target.position - self.position).normalized;

        float timer = 0f;
        while (timer < dashDuration)
        {
            rb.velocity = direction * dashSpeed;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        isAttacking = false;
    }
}
