using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonData : MonoBehaviour
{
    public enum UpgradeType
    {
        Health,
        Damage,
        Speed,
        Cooldown
    }

    [Header("upgrade info")]
    public int cost;
    public float value;
    public UpgradeType type;

    [Header("core")]
    public Button button;

    [Header("ui")]
    public Sprite icon;

    [Header("purchased visual")]
    public GameObject purchasedBG;

    [HideInInspector] public bool isPurchased = false;

    //key playerprefs tự động dựa vào index trong parent manager
    private string PurchaseKey => $"UpgradeButton_{buttonIndex}_Purchased";

    //index của nút trong mảng buttons
    private int buttonIndex;

    //gọi bởi manager khi khởi tạo
    public void LoadButtonPurchaseState(int index)
    {
        buttonIndex = index;
        LoadPurchaseState();
    }

    //load trạng thái nút
    public void LoadPurchaseState()
    {
        isPurchased = PlayerPrefs.GetInt(PurchaseKey, 0) == 1;
    }

    //lưu trạng thái khi mua
    public void SavePurchaseState()
    {
        PlayerPrefs.SetInt(PurchaseKey, isPurchased ? 1 : 0);
        PlayerPrefs.Save();
    }

    //reset về trạng thái chưa mua
    public void ResetPurchaseState()
    {
        isPurchased = false;
        PlayerPrefs.DeleteKey(PurchaseKey);
    }
}
