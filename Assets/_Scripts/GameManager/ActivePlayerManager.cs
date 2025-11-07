using UnityEngine;

public class ActivePlayerManager : Singleton<ActivePlayerManager>
{
    public GameObject CurrentPlayerPrefab;
    public GameObject CurrentPlayerInstance;
    [Header("Prefabs Setup")]
    public GameObject[] allPrefabs; // gán prefab trong Inspector

    private const string PrefabIndexKey = "CurrentPlayerPrefabIndex";

    private void Update()
    {
        // Nhấn R → reset tất cả 
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCurrentPlayerPrefab();
        }
    }


    private void Start()
    {
        LoadCurrentPlayerPrefab();
    }
    // gọi từ UI menu
    public void SetCurrent(GameObject prefab)
    {
        CurrentPlayerPrefab = prefab;

        //lưu index prefab vào PlayerPrefs
        int index = System.Array.IndexOf(allPrefabs, prefab);
        if (index >= 0)
        {
            PlayerPrefs.SetInt(PrefabIndexKey, index);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Gọi khi spawn trong gameplay
    /// </summary>
    public void SetInstance(GameObject instance)
    {
        CurrentPlayerInstance = instance;
    }

    /// <summary>
    /// Load prefab đã lưu từ PlayerPrefs
    /// </summary>
    public void LoadCurrentPlayerPrefab()
    {
        int index = PlayerPrefs.GetInt(PrefabIndexKey, 0); // mặc định prefab đầu tiên
        if (allPrefabs != null && allPrefabs.Length > index)
        {
            CurrentPlayerPrefab = allPrefabs[index];
        }
    }

    public void ResetCurrentPlayerPrefab()
    {
        PlayerPrefs.DeleteKey(PrefabIndexKey);
        PlayerPrefs.Save();
        CurrentPlayerPrefab = allPrefabs.Length > 0 ? allPrefabs[0] : null;

        Debug.Log("Reset Current PlayerPrefab");
    }
}
