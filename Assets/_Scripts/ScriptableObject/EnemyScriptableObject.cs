using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    //Base stats for the enemy
    public float enemyMoveSpeed;
    public float enemyMaxHealth;
    public float enemyDamage;
}