using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text _timerText;
    public static float _curentTime;
    public static float _timeBossDie;
    void Start()
    {
        //BossHealth.elapsedTime = 0;
        _curentTime = Time.timeSinceLevelLoad; /*- BossHealth.elapsedTime;*/
        _timerText = GetComponent<Text>();
        UpdateTimeToDisplay();
    }

    void Update()
    {
        _curentTime = Time.timeSinceLevelLoad;/* - BossHealth.elapsedTime*/;
        UpdateTimeToDisplay();
    }

    private void UpdateTimeToDisplay()
    {
        int minutes = Mathf.FloorToInt(_curentTime / 60);
        int seconds = Mathf.FloorToInt(_curentTime % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        _timerText.text = timeString;
    }
}
