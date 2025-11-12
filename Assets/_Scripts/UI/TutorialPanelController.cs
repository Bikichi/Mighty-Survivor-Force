using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPanelController : MonoBehaviour
{
    [Header("Tutorial Panels")]
    public GameObject tutorialTouchJoypadPanel;
    public GameObject tutorialAutoShootPanel;
    public GameObject tutorialSelectSkillPanel;

    [Header("External Reference")]
    public GameObject skillSelectionPanel;

    [HideInInspector] public TutorialManager manager;

    private int currentStep = 0;
    [SerializeField] private bool waitingForTouch = false;

    public void StartTutorial()
    {
        HideAllPanels();
        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence()
    {
        yield return new WaitForSeconds(0.5f);
        ShowPanel(tutorialTouchJoypadPanel);
        currentStep = 1;
    }

    void Update()
    {
        if (waitingForTouch && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            HideCurrentPanel();
        }

        if (currentStep == 3 && skillSelectionPanel.activeSelf && !tutorialSelectSkillPanel.activeSelf)
        {
            ShowPanel(tutorialSelectSkillPanel);
        }
    }

    void ShowPanel(GameObject panel)
    {
        AudioController.Instance.PlaySound(AudioController.Instance.toturialSound);
        Time.timeScale = 0f;
        panel.SetActive(true);
        waitingForTouch = true;
    }

    void HideCurrentPanel()
    {
        waitingForTouch = false;
        Time.timeScale = 1f;

        switch (currentStep)
        {
            case 1:
                tutorialTouchJoypadPanel.SetActive(false);
                StartCoroutine(ShowNextAfterDelay(tutorialAutoShootPanel, 0.5f));
                currentStep = 2;
                break;

            case 2:
                tutorialAutoShootPanel.SetActive(false);
                currentStep = 3;
                break;

            case 3:
                tutorialSelectSkillPanel.SetActive(false);
                manager.EndTutorial();
                break;
        }
    }

    IEnumerator ShowNextAfterDelay(GameObject nextPanel, float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowPanel(nextPanel);
    }

    void HideAllPanels()
    {
        tutorialTouchJoypadPanel.SetActive(false);
        tutorialAutoShootPanel.SetActive(false);
        tutorialSelectSkillPanel.SetActive(false);
    }
}
