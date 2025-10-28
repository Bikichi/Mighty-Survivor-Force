using UnityEngine;

public class AttackMassController : MonoBehaviour
{
    public Rigidbody rb;
    public EnemyAttack enemyAttack;
    public SpecialAttack_Dash specialDash;
    public SpecialAttack_Explode specialExplode;

    [Header("Mass Settings")]
    public float normalMass;
    public float attackMass = 10000f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        specialDash = GetComponent<SpecialAttack_Dash>();
        specialExplode = GetComponent<SpecialAttack_Explode>();
        normalMass = rb.mass;
    }

    void Update()
    {
        bool isAnyAttack =
            (enemyAttack != null && enemyAttack.isAttacking) ||
            (specialDash != null && specialDash.IsAttacking) ||
            (specialExplode != null && specialExplode.HasExploded);

        rb.mass = isAnyAttack ? attackMass : normalMass;
    }
}
