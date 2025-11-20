using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Leaderboards;
using UnityEngine.SceneManagement;

public class LeaderboardsMenu : Panel
{
    [SerializeField] private int playersPerPage = 25;
    [SerializeField] private LeaderboardsPlayerItem playerItemPrefab = null;
    [SerializeField] private RectTransform playersContainer = null;
    [SerializeField] private TextMeshProUGUI pageText = null;
    [SerializeField] private Button nextButton = null;
    [SerializeField] private Button prevButton = null;
    [SerializeField] private Button closeButton = null;
    [SerializeField] private GameObject asyncLoaderPrefab;
    private int lastRecordedKills = 0;

    private int currentPage = 1;
    private int totalPages = 0;

    public override void Initialize()
    {
        if (IsInitialized) return;

        ClearPlayersList();
        closeButton.onClick.AddListener(ClosePanel);
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);

        base.Initialize();
    }

    private void Start()
    {
        Open();
    }

    public override void Open()
    {
        // Lấy tổng kills hiện tại từ ScoreManager
        lastRecordedKills = PlayerPrefs.GetInt("LastRecordedKills", 0);

        int totalKills = ScoreManager.Instance.GetTotalKills();
        int newKills = totalKills - lastRecordedKills;

        //Debug.Log($"TotalKills = {totalKills}, LastRecordedKills = {lastRecordedKills}, NewKills = {newKills}");

        newKills = Mathf.Max(newKills, 0);

        AddScoreAsync(newKills);

        // Cập nhật lại LastRecordedKills để lần sau không cộng nữa
        PlayerPrefs.SetInt("LastRecordedKills", ScoreManager.Instance.GetTotalKills());
        PlayerPrefs.Save();

        pageText.text = "-";
        nextButton.interactable = false;
        prevButton.interactable = false;
        base.Open();
        ClearPlayersList();
        currentPage = 1;
        totalPages = 0;
        LoadPlayers(currentPage);
    }

    private async void AddScoreAsync(int score)
    {
        try
        {
            var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync("Top_Killers", score);
            LoadPlayers(currentPage);
        }
        catch (Exception ex)
        {
            Debug.LogError("Lỗi khi thêm điểm: " + ex.Message);
        }
    }

    private async void LoadPlayers(int page)
    {
        nextButton.interactable = false;
        prevButton.interactable = false;
        try
        {
            GetScoresOptions options = new GetScoresOptions
            {
                Offset = (page - 1) * playersPerPage,
                Limit = playersPerPage
            };
            var scores = await LeaderboardsService.Instance.GetScoresAsync("Top_Killers", options);
            ClearPlayersList();

            foreach (var result in scores.Results)
            {
                LeaderboardsPlayerItem item = Instantiate(playerItemPrefab, playersContainer);
                item.Initialize(result);
            }

            totalPages = Mathf.CeilToInt((float)scores.Total / (float)scores.Limit);
            currentPage = page;
        }
        catch (Exception ex)
        {
            Debug.LogError("Lỗi khi load leaderboard: " + ex.Message);
        }

        pageText.text = $"{currentPage}/{totalPages}";
        nextButton.interactable = currentPage < totalPages && totalPages > 1;
        prevButton.interactable = currentPage > 1 && totalPages > 1;
    }

    private void NextPage()
    {
        LoadPlayers(currentPage + 1 > totalPages ? 1 : currentPage + 1);
    }

    private void PrevPage()
    {
        LoadPlayers(currentPage - 1 <= 0 ? totalPages : currentPage - 1);
    }

    private void ClosePanel()
    {
        base.Close(); // Tắt panel hiện tại
        if (asyncLoaderPrefab != null)
        {
            GameObject loaderGO = Instantiate(asyncLoaderPrefab);
            AsyncSceneLoader loader = loaderGO.GetComponent<AsyncSceneLoader>();
            loader.StartLoadScene("_MainMenuScene");
        }
        else
        {
            SceneManager.LoadScene("_MainMenuScene");
        }

    }

    private void ClearPlayersList()
    {
        foreach (Transform child in playersContainer)
        {
            Destroy(child.gameObject);
        }
    }

}
