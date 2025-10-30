using System.Collections;
using UnityEngine;

public class ChildEnemyHealth : EnemyHealth
{
    protected override void Die()
    {
        IsActive = false;
        IsDead = true;
        onDeath?.Invoke();
        DisableEnemyActions();
        StartCoroutine(HandleDeath());

        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();

    }
}
