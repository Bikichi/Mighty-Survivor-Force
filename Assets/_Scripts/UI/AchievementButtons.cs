using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievementButtons : MonoBehaviour
{
    [SerializeField] private Button openAchievementButton;
    [SerializeField] private string achievementSceneName = "Achievement"; // tên scene Achievement

    private void Awake()
    {
        if (openAchievementButton != null)
        {
            openAchievementButton.onClick.AddListener(OpenAchievementScene);
        }
        else
        {
            Debug.LogWarning("Button chưa được gán trong AchievementButtons!");
        }
    }

    private void OpenAchievementScene()
    {
        if (!string.IsNullOrEmpty(achievementSceneName))
        {
            SceneManager.LoadScene(achievementSceneName);
        }
        else
        {
            Debug.LogWarning("Tên Scene Achievement chưa được đặt!");
        }
    }

}
