using UnityEngine;
using UnityEngine.UI;

public class AudioButtonHandler : MonoBehaviour
{
    [Header("Images to change color")]
    public Image image1;
    public Image image2;

    [Header("Colors for ON/OFF states")]
    public Color onColor = Color.green;  // Bật
    public Color offColor = Color.red;   // Tắt

    [Header("Toggle type")]
    public bool isMusicButton = true; // true = music, false = SFX

    public void OnToggle()
    {
        if (isMusicButton)
        {
            AudioController.Instance.ToggleMusic();
            UpdateImages(AudioController.Instance.musicVolume > 0f);
        }
        else
        {
            AudioController.Instance.ToggleSFX();
            UpdateImages(AudioController.Instance.sfxVolume > 0f);
        }
    }

    private void UpdateImages(bool isOn)
    {
        if (image1 != null)
            image1.color = isOn ? onColor : offColor;
        if (image2 != null)
            image2.color = isOn ? onColor : offColor;
    }

    private void Start()
    {
        if (isMusicButton)
            UpdateImages(AudioController.Instance.musicVolume > 0f);
        else if (!isMusicButton)
            UpdateImages(AudioController.Instance.sfxVolume > 0f);
    }
}
