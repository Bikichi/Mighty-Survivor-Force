using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timerTextInGameplay;
    public TMP_Text timerTextInGameCompleted;
    public TMP_Text bestTimeTextInGameCompleted;

    [SerializeField] public BestTimeManager bestTimeManager;
    private float elapsedTime = 0f;
    private bool isRunning = true;

    private void Start()
    {
        bestTimeManager = FindAnyObjectByType<BestTimeManager>();
        // load best time lên UI trong bảng hoàn thành
        if (bestTimeTextInGameCompleted != null)
        {
            float best = bestTimeManager.GetBestTime();
            bestTimeTextInGameCompleted.text = "Best " + bestTimeManager.FormatTime(best);
        }
    }

    private void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        string formatted = bestTimeManager.FormatTime(elapsedTime);

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
        bestTimeManager.SaveIfBest(elapsedTime);

        // cập nhật lại best time sau khi lưu
        if (bestTimeTextInGameCompleted != null)
        {
            float best = bestTimeManager.GetBestTime();
            bestTimeTextInGameCompleted.text = "BEST " + bestTimeManager.FormatTime(best);
        }
    }

    public void ResetBestTime()
    {
        bestTimeManager.ResetBestTime();
    }
}
