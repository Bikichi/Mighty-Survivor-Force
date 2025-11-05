using UnityEngine;

public class MainPanelController : MonoBehaviour
{
    [Header("Main UI Panels")]
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private GameObject playPanel;

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
    public void ToggleHeroPanel() => TogglePanel(heroPanel);
    public void ToggleUpgradesPanel() => TogglePanel(upgradesPanel);
    public void TogglePlayPanel() => TogglePanel(playPanel);
}
