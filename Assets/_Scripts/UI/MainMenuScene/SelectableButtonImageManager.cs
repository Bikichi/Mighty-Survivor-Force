using UnityEngine;
using UnityEngine.UI;

public class SelectableButtonImageManager : MonoBehaviour
{
    [System.Serializable]
    public class SelectableButton
    {
        public Button button;              //nút chính
        public Image backgroundImage;      //ảnh nền chính
        public GameObject extraBgObject;   //background phụ bật/tắt
        public Sprite normalSprite;        //ảnh mặc định
        public Sprite activeSprite;        //ảnh khi được chọn
    }

    [Header("Button Setup")]
    public SelectableButton[] buttons;

    protected int currentIndex = -1;

    protected virtual void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].button.onClick.AddListener(() => SetActiveButton(index));
        }

        // Chọn nút giữa làm mặc định
        if (buttons.Length > 0)
        {
            int middleIndex = buttons.Length / 2;
            SetActiveButton(middleIndex);
        }
    }

    /// <summary>
    /// Bật nút, đổi sprite và background phụ
    /// </summary>
    public virtual void SetActiveButton(int index)
    {
        if (index == currentIndex) return;

        currentIndex = index;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].backgroundImage != null)
                buttons[i].backgroundImage.sprite =
                    (i == index) ? buttons[i].activeSprite : buttons[i].normalSprite;

            if (buttons[i].extraBgObject != null)
                buttons[i].extraBgObject.SetActive(i == index);
        }
    }
}
