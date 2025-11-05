using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterButtonImageManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterButton
    {
        public Button button;              // Nút chính
        public Image backgroundImage;      // Ảnh nền chính
        public GameObject extraBgObject;   // Background phụ bật/tắt
        public TMP_Text extraBgText;           // Text hiển thị LOCKED / SELECTED
        public Sprite normalSprite;        // Ảnh mặc định
        public Sprite activeSprite;        // Ảnh khi được chọn
        public bool isLocked = false;      // Nút bị lock hay không
    }

    [Header("Character Button Setup")]
    public CharacterButton[] buttons;

    private int currentIndex = -1;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // tránh closure bug
            buttons[i].button.onClick.AddListener(() => SetActiveButton(index));
        }

        // Chọn nút giữa làm mặc định (nếu không bị lock)
        if (buttons.Length > 0)
        {
            int middleIndex = buttons.Length / 2;
            SetActiveButton(middleIndex);
        }
    }

    /// <summary>
    /// Bật nút, đổi sprite và background phụ
    /// </summary>
    public void SetActiveButton(int index)
    {
        if (index == currentIndex) return;

        currentIndex = index;

        for (int i = 0; i < buttons.Length; i++)
        {
            // Đổi sprite
            if (buttons[i].backgroundImage != null)
                buttons[i].backgroundImage.sprite =
                    (i == index) ? buttons[i].activeSprite : buttons[i].normalSprite;

            // Bật/tắt background phụ
            if (buttons[i].extraBgObject != null)
                buttons[i].extraBgObject.SetActive(i == index);

            // Cập nhật text LOCKED / SELECTED
            if (buttons[i].extraBgText != null)
            {
                if (buttons[i].isLocked)
                    buttons[i].extraBgText.text = "LOCKED";
                else
                    buttons[i].extraBgText.text = (i == index) ? "SELECTED" : "";
            }
        }
    }
}
