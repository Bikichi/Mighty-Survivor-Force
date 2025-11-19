using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timerTextInGameplay;
    public TMP_Text timerTextInGameCompleted;
    public TMP_Text bestTimeTextInGameCompleted;
    public TMP_Text bestTimeTextInGameOver;
    [SerializeField] public BestTimeManager bestTimeManager;
    private float elapsedTime = 0f;
    private bool isRunning = true;

    private void Start()
    {
        bestTimeManager = FindAnyObjectByType<BestTimeManager>();

        // load best time lên UI trong bảng hoàn thành
        if (bestTimeTextInGameCompleted != null)
        {
            float best = bestTimeManager.GetBestTimeCurrentScene();
            bestTimeTextInGameCompleted.text = "BEST " + bestTimeManager.FormatTime(best);
        }

        // load best time lên UI trong bảng Game Over
        if (bestTimeTextInGameOver != null)
        {
            float best = bestTimeManager.GetBestTimeCurrentScene();
            bestTimeTextInGameOver.text = "BEST " + bestTimeManager.FormatTime(best);
        }
    }

    private void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        string formatted = bestTimeManager.FormatTime(elapsedTime);

        if (timerTextInGameplay != null)
            timerTextInGameplay.text = formatted;

        if (timerTextInGameCompleted != null)
            timerTextInGameCompleted.text = formatted;
    }

    public void StopTimer()
    {
        if (!isRunning) return;

        isRunning = false;

        // lưu best time cho scene hiện tại
        bestTimeManager.SaveIfBest(elapsedTime);

        // cập nhật lại best time sau khi lưu
        float best = bestTimeManager.GetBestTimeCurrentScene();
        if (bestTimeTextInGameCompleted != null)
            bestTimeTextInGameCompleted.text = "BEST " + bestTimeManager.FormatTime(best);

        if (bestTimeTextInGameOver != null)
            bestTimeTextInGameOver.text = "BEST " + bestTimeManager.FormatTime(best);
    }

    public void ResetBestTime()
    {
        bestTimeManager.ResetBestTime();
    }


}
