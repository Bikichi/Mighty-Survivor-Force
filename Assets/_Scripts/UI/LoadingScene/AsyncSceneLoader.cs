using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    public ProgressBar progress;
    public float fakeDuration;

    private AsyncOperation loadingOperation;
    private float startTime;

    private string targetSceneName;

    public void StartLoadScene(string sceneName)
    {
        targetSceneName = sceneName;

        DontDestroyOnLoad(this);
        startTime = Time.unscaledTime;
        loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        //nếu muốn fake progress mà không ảnh hưởng UI khác, có thể bỏ Time.timeScale = 0
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (loadingOperation == null) return;

        float fakeProgress = (Time.unscaledTime - startTime) / fakeDuration;
        float finalProgress = Mathf.Min(fakeProgress, loadingOperation.progress);
        progress.SetProgressValue(finalProgress);

        if (loadingOperation.isDone && finalProgress >= 1f)
        {
            FinishLoading();
        }
    }

    private void FinishLoading()
    {
        Time.timeScale = 1;
        Destroy(gameObject);

        PlayMusicByScene(targetSceneName);
    }

    private void PlayMusicByScene(string sceneName)
    {
        // Nếu tên scene chứa "Menu" → bật nhạc menu
        if (sceneName.Contains("_MainMenuScene"))
        {
            AudioController.Instance.PlayMusic(AudioController.Instance.menuBackgroundMusics, true);
        }
        else
        {
            AudioController.Instance.PlaySound(AudioController.Instance.waveAlertSound);
            AudioController.Instance.PlayMusic(AudioController.Instance.gamePlayBackgroundMusics, true);
        }
    }
}
