using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int totalKills;
    public static ScoreManager Instance { get; private set; }

    void Awake()
    {
        //đảm bảo chỉ có 1 AdsInitializer duy nhất
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        totalKills = PlayerPrefs.GetInt("TotalKills", 0);

    }

    public int GetTotalKills()
    {
        return totalKills;
    }

    public void AddKill(int count = 1)
    {
        totalKills += count;
        PlayerPrefs.SetInt("TotalKills", totalKills);
        PlayerPrefs.Save();  // Lưu ngay
        Debug.Log("Tổng số quái đã tiêu diệt: " + totalKills);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetKills();
        }
    }

    public void ResetKills()
    {
        Debug.Log("ResetKills() được gọi — Reset totalKills và lastRecordedKills!");

        totalKills = 0;
        PlayerPrefs.SetInt("TotalKills", 0);
        PlayerPrefs.SetInt("PlayerNameUIVisible", 0);
        PlayerPrefs.SetInt("LastRecordedKills", 0); // reset luôn
        PlayerPrefs.Save();
    }

}
