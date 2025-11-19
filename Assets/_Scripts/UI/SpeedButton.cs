using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedButton : MonoBehaviour
{
    [SerializeField] private Button speedButton;
    [SerializeField] private TMP_Text speedText;

    private int currentSpeedIndex = 0;
    private float[] speedLevels = { 1f, 2f, 3f };

    public static float CurrentSpeed = 1f;

    private void Start()
    {
        speedButton.onClick.AddListener(ChangeSpeed);
        //ApplySpeed();
    }

    private void ChangeSpeed()
    {
        currentSpeedIndex++;

        if (currentSpeedIndex >= speedLevels.Length)
            currentSpeedIndex = 0;

        ApplySpeed();
    }

    private void ApplySpeed()
    {
        CurrentSpeed = speedLevels[currentSpeedIndex];
        Time.timeScale = CurrentSpeed;
        speedText.text = "x" + CurrentSpeed.ToString("0");
    }
}
