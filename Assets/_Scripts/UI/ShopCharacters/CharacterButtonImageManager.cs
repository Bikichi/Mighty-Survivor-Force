using UnityEngine;
using UnityEngine.UI;
using TMPro; // thêm TextMeshPro

public class CharacterButtonImageManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonVisual
    {
        public Button button;
        public Image backgroundImage;     // Sprite nền khi chọn
        public Sprite normalSprite;
        public Sprite activeSprite;

        public GameObject extraBgObject;  // panel dưới SELECTED / LOCKED
        public TMP_Text extraBgText;      // đổi sang TextMeshPro

        public GameObject lockPanel;      // panel hiển thị khi bị khoá
    }

    [Header("Button Setup")]
    public ButtonVisual[] buttons;

    [Header("Character Logic")]
    public CharacterSelectionManager selectionManager;

    private int currentIndex = -1;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].button.onClick.AddListener(() => OnButtonClicked(index));
        }

        OnButtonClicked(0); // chọn nút đầu tiên
    }

    private void OnButtonClicked(int index)
    {
        SetActiveButton(index);
        selectionManager.SelectCharacter(index);
    }

    public void SetActiveButton(int index)
    {
        if (index == currentIndex)
            return;

        currentIndex = index;

        for (int i = 0; i < buttons.Length; i++)
        {
            var b = buttons[i];

            b.backgroundImage.sprite = (i == index) ? b.activeSprite : b.normalSprite;

            bool isLocked = selectionManager.characters[i].isUnlocked;

            if (b.extraBgObject != null)
            {
                bool active = i == index;
                b.extraBgObject.SetActive(active);

                if (active)
                    b.extraBgText.text = isLocked ? "LOCKED" : "SELECTED";
            }

            if (b.lockPanel != null)
                b.lockPanel.SetActive(isLocked);
        }
    }
}
