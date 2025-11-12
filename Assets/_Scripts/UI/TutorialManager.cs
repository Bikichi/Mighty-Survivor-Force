using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialPanelController panelController;
    private const string TutorialShownKey = "TutorialShown";
    [SerializeField] private bool iWantToReset;

    void Start()
    {
        if (iWantToReset)
        {
            ResetTutorial();
        }
        // Nếu đã xem rồi thì tắt tutorial
        if (PlayerPrefs.GetInt(TutorialShownKey, 0) == 1)
        {
            gameObject.SetActive(false);
            return;
        }

        // Bắt đầu tutorial
        panelController.manager = this;
        panelController.StartTutorial();
    }

    public void EndTutorial()
    {
        PlayerPrefs.SetInt(TutorialShownKey, 1);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }

    public void ResetTutorial()
    {
        PlayerPrefs.DeleteKey(TutorialShownKey);
        PlayerPrefs.Save();

        // Bật lại tutorial
        gameObject.SetActive(true);
        Debug.Log("Tutorial has been reset!");
    }

}
