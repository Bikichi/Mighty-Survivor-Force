using UnityEngine;

public abstract class SpecialAttackBehavior : MonoBehaviour
{
    /// <summary>
    /// Thực hiện hành vi tấn công đặc biệt lên mục tiêu.
    /// </summary>
    public abstract void ExecuteSpecialAttack(Transform target);
}