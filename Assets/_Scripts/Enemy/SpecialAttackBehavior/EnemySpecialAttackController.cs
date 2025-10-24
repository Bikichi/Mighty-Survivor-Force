using UnityEngine;

public class EnemySpecialAttackController : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform player;

    [Header("Activation Settings")]
    [SerializeField] private float triggerRange = 5f;
    [SerializeField] private float cooldownTime = 5f;

    [Header("Assigned Special Attack")]
    [SerializeField] private SpecialAttackBehavior specialAttack;

    private float lastAttackTime;
    private bool isCoolingDown => Time.time < lastAttackTime + cooldownTime;

    private void Update()
    {
        if (player == null || specialAttack == null)
            return;

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
