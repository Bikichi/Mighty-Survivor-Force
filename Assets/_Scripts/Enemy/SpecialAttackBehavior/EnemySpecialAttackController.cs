using UnityEngine;

public class EnemySpecialAttackController : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform player;

    [Header("Activation Settings")]
    [SerializeField] private float triggerRange;
    [SerializeField] private float cooldownTime;

    [Header("Assigned Special Attack")]
    [SerializeField] private SpecialAttackBehavior specialAttack;

    private float lastAttackTime;
    private bool isCoolingDown => Time.time < lastAttackTime + cooldownTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= triggerRange && !isCoolingDown)
        {
            specialAttack.ExecuteSpecialAttack(player);
            lastAttackTime = Time.time;
        }
    }

    public void ForceActivateSpecialAttack()
    {
        if (specialAttack != null)
        {
            specialAttack.ExecuteSpecialAttack(player);
            lastAttackTime = Time.time;
        }
    }
}
