using UnityEngine;

public class BestTimeManager : MonoBehaviour
{
    private const string BEST_TIME_KEY = "BestTime";

    public float GetBestTime()
    {
        return PlayerPrefs.GetFloat(BEST_TIME_KEY, float.MaxValue);
    }

    public void SaveIfBest(float newTime)
    {
        float best = GetBestTime();

        if (newTime < best)
        {
            PlayerPrefs.SetFloat(BEST_TIME_KEY, newTime);
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
        PlayerPrefs.DeleteKey(BEST_TIME_KEY);
    }
}
