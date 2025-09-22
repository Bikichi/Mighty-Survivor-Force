using UnityEngine;

public class AttackMassController : MonoBehaviour
{
    public Rigidbody rb;
    public EnemyAttack enemyAttack;

    [Header("Mass Settings")]
    public float normalMass;       //khối lượng bình thường
    public float attackMass = 10000f;    //khối lượng khi tấn công

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
    }

    void Update()
    {
        rb.mass = enemyAttack.isAttacking ? attackMass : normalMass;
    }
}
