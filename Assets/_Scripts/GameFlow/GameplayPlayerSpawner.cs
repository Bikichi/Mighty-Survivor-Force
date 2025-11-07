using UnityEngine;

public class GameplayPlayerSpawner : MonoBehaviour
{
    private Vector3 spawnPosition = new Vector3(0.2f, 1.58f, -2.5f);
    private Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);

    private void Awake()
    {
        if (ActivePlayerManager.CurrentPlayerPrefab == null)
        {
            Debug.LogError("No character selected!");
            return;
        }

        // Spawn player và lưu instance vào ActivePlayerManager
        GameObject playerInstance = Instantiate(
            ActivePlayerManager.CurrentPlayerPrefab,
            spawnPosition,
            spawnRotation
        );

        ActivePlayerManager.SetInstance(playerInstance);
    }
}
