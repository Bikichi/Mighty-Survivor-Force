using UnityEngine;

public class AchievementButtons : MonoBehaviour
{
    [SerializeField] private AchievenmentListIngame achievementUI;

    private void Start()
    {
        achievementUI = FindObjectOfType<AchievenmentListIngame>();
    }

    public void Open()
    {
        achievementUI.OpenWindow();
    }

    public void Close()
    {
        achievementUI.CloseWindow();
    }

    public void Toggle()
    {
        achievementUI.ToggleWindow();
    }
}
