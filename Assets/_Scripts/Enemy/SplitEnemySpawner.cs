using UnityEngine;

public class SplitEnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject childEnemyPrefab;
    [SerializeField] private float spawnOffset = 2f; // khoảng cách 2 bên

    public void SpawnChildren(Vector3 parentPosition, Quaternion parentRotation)
    {
        if (childEnemyPrefab == null)
        {
            return;
        }
        Vector3 rightOffset = parentRotation * Vector3.right * spawnOffset;
        Vector3 leftOffset = parentRotation * Vector3.left * spawnOffset;

        Instantiate(childEnemyPrefab, parentPosition + rightOffset, parentRotation);
        Instantiate(childEnemyPrefab, parentPosition + leftOffset, parentRotation);
    }
}
