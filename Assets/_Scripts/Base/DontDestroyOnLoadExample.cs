using UnityEngine;

public class DontDestroyOnLoadExample : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
