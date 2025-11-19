using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    public TMP_Text leaderboardText;
    public string[] sceneNames; // danh sách các scene muốn show best time
    private BestTimeManager bestTimeManager;

    private void Start()
    {
        bestTimeManager = FindAnyObjectByType<BestTimeManager>();
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        if (leaderboardText == null) return;

        leaderboardText.text = "";

        foreach (string scene in sceneNames)
        {
            float best = bestTimeManager.GetBestTimeForScene(scene);
            string formatted = bestTimeManager.FormatTime(best);
            leaderboardText.text += $"{scene}: {formatted}\n";
        }
    }
}
