using UnityEngine;
using UnityEngine.SceneManagement;

public class BestTimeManager : MonoBehaviour
{
    private const string BEST_TIME_KEY_PREFIX = "BestTime_";


    private string GetKeyForScene(string sceneName)
    {
        return BEST_TIME_KEY_PREFIX + sceneName;
    }

    private string GetKeyForCurrentScene()
    {
        return GetKeyForScene(SceneManager.GetActiveScene().name);
    }

    public float GetBestTimeCurrentScene()
    {
        string key = GetKeyForCurrentScene();
        return PlayerPrefs.GetFloat(key, float.MaxValue);
    }

    public float GetBestTimeForScene(string sceneName)
    {
        string key = GetKeyForScene(sceneName);
        return PlayerPrefs.GetFloat(key, float.MaxValue);
    }

    public void SaveIfBest(float newTime)
    {
        string key = GetKeyForCurrentScene();
        float best = GetBestTimeCurrentScene();

        if (newTime < best)
        {
            PlayerPrefs.SetFloat(key, newTime);
            PlayerPrefs.Save();
        }
    }

    public string FormatTime(float time)
    {
        if (time == float.MaxValue) return "--:--";

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        return $"{minutes:00}:{seconds:00}";
    }

    public void ResetBestTime()
    {
        string key = GetKeyForCurrentScene();
        PlayerPrefs.DeleteKey(key);
    }

    public void ResetBestTimeForScene(string sceneName)
    {
        string key = GetKeyForScene(sceneName);
        PlayerPrefs.DeleteKey(key);
    }

}
