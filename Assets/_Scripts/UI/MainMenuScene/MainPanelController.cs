using UnityEngine;

public class MainPanelController : MonoBehaviour
{
    [Header("Main UI Panels")]
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private GameObject playPanel;

    [Header("References")]
    [SerializeField] private CharacterSelectionManager characterSelectionManager;

    public void TogglePanel(GameObject panel)
    {
        GameObject[] panels = { heroPanel, upgradesPanel, playPanel };

        //nếu panel được nhấn đang bật thì không làm gì
        if (panel.activeSelf)
            return;

        //ẩn tất cả panel khác
        foreach (GameObject p in panels)
        {
            if (p != null)
                p.SetActive(false);
        }

        //bật panel được chọn
        panel.SetActive(true);
    }

    //hàm gọi trực tiếp từ button
    public void ToggleHeroPanel()
    {
        TogglePanel(heroPanel);

        //load dữ liệu nhân vật ngay khi mở panel hero
        if (heroPanel.activeSelf)
        {
            characterSelectionManager.LoadUnlockStates();                     // load trạng thái unlock
            int savedIndex = characterSelectionManager.LoadSelectedCharacter(); // load nhân vật đã chọn
            characterSelectionManager.SelectCharacter(savedIndex);            // cập nhật UI
            characterSelectionManager.GetComponent<CharacterButtonImageManager>().SetActiveButton(savedIndex); //động bộ nút tương ứng với nhân vật đã chọn
            //tham chiếu tới Component CharacterButtonImageManager ở GameObject mà MainPanelController đang tham chiếu CharacterSelectionManager
            //vì CharacterButtonImageManager và CharacterSelectionManager cùng gắn trên 1 game object
        }
    }
    public void ToggleUpgradesPanel() => TogglePanel(upgradesPanel);
    public void TogglePlayPanel() => TogglePanel(playPanel);
}
