using UnityEngine;

public class ActivePlayerManager : MonoBehaviour
{
    public static ActivePlayerManager Instance;

    public static GameObject CurrentPlayerPrefab;   // chỉ lưu prefab
    public static GameObject CurrentPlayerInstance; // instance thực tế đang chạy

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Set prefab được chọn từ menu
    public static void SetCurrent(GameObject prefab)
    {
        CurrentPlayerPrefab = prefab;
    }

    // Gán instance sau khi spawn
    public static void SetInstance(GameObject instance)
    {
        CurrentPlayerInstance = instance;
    }
}
