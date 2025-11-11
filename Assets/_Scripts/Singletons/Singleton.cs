using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected bool persistAcrossScenes = true;

    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T instanceInScene = FindAnyObjectByType<T>();
                RegisterInstance(instanceInScene);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            RegisterInstance((T)(MonoBehaviour)this);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private static void RegisterInstance(T newInstance)
    {
        if (newInstance == null) return;

        _instance = newInstance;

        // ✅ CHỈ giữ lại nếu chọn persist
        var singleton = newInstance as Singleton<T>;
        if (singleton != null && singleton.persistAcrossScenes)
        {
            DontDestroyOnLoad(singleton.gameObject);
        }
    }
}
