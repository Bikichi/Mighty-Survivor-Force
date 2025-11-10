using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject skillPanel;
    [SerializeField] private GameObject waveAlertUI;

    [Header("Game State Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameCompletePanel;

    #region Skill Panel
    public void ShowSkillPanel()
    {
        Time.timeScale = 0f;
        skillPanel.SetActive(true);
    }

    public void HideSkillPanel()
    {
        Time.timeScale = 1f;
        skillPanel.SetActive(false);
    }
    #endregion

    #region Wave Alert
    public void ShowWaveAlert()
    {
        waveAlertUI.SetActive(true);
    }

    public void HideWaveAlert()
    {
        waveAlertUI.SetActive(false);
    }
    #endregion

    #region Pause
    public void ShowPausePanel()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        // chỉ resume nếu skillPanel không bật
        if (!skillPanel.activeSelf)
        {
            Time.timeScale = 1f;
        }
        pausePanel.SetActive(false);
    }

    // Toggle Pause (có thể gọi khi nhấn nút Escape)
    public void TogglePause()
    {
        if (pausePanel.activeSelf)
            HidePausePanel();
        else
            ShowPausePanel();
    }
    #endregion

    #region Game Over
    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void HideGameOver()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
    }
    #endregion

    #region Game Complete
    public void ShowGameComplete()
    {
        Time.timeScale = 0f;
        FindAnyObjectByType<TimerUI>().StopTimer();
        gameCompletePanel.SetActive(true);
    }
    public void HideGameComplete()
    {
        Time.timeScale = 1f;
        gameCompletePanel.SetActive(false);
    }

    #endregion

    public void ExitToHome()
    {
        Time.timeScale = 1f;

        // Load scene Main Menu (đổi tên scene nếu khác)
        SceneManager.LoadScene("_MainMenuScene");
    }
}
