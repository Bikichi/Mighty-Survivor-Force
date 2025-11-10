using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timerTextInGameplay;
    public TMP_Text timerTextInGameCompleted;
    public TMP_Text bestTimeTextInGameCompleted;

    private float elapsedTime = 0f;
    private bool isRunning = true;

    private void Start()
    {
        // load best time lên UI trong bảng hoàn thành
        if (bestTimeTextInGameCompleted != null)
        {
            float best = BestTimeManager.Instance.GetBestTime();
            bestTimeTextInGameCompleted.text = "Best " + BestTimeManager.Instance.FormatTime(best);
        }
    }

    private void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        string formatted = BestTimeManager.Instance.FormatTime(elapsedTime);

        if (timerTextInGameplay != null)
            timerTextInGameplay.text = formatted;

        //Update ngay cả khi gameobject chứa bestTimeTextInGameCompleted không enable
        if (timerTextInGameCompleted != null)
            timerTextInGameCompleted.text = formatted;
    }

    public void StopTimer()
    {
        if (!isRunning) return;

        isRunning = false;

        // lưu best time
        BestTimeManager.Instance.SaveIfBest(elapsedTime);

        // cập nhật lại best time sau khi lưu
        if (bestTimeTextInGameCompleted != null)
        {
            float best = BestTimeManager.Instance.GetBestTime();
            bestTimeTextInGameCompleted.text = "BEST " + BestTimeManager.Instance.FormatTime(best);
        }
    }

    public void ResetBestTime()
    {
        BestTimeManager.Instance.ResetBestTime();
    }
}
