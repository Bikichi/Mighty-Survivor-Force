using UnityEngine;

public class AttackMassController : MonoBehaviour
{
    public Rigidbody rb;
    public EnemyAttack enemyAttack;

    [Header("Mass Settings")]
    public float normalMass = 10f;       //khối lượng bình thường
    public float attackMass = 10000f;    //khối lượng khi tấn công

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (enemyAttack == null)
            enemyAttack = GetComponentInChildren<EnemyAttack>();
    }

    void Update()
    {
        if (enemyAttack == null || rb == null) return;
        rb.mass = enemyAttack.isAttacking ? attackMass : normalMass;
    }
}
