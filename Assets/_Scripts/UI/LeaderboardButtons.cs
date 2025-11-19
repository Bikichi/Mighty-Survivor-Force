using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardButtons : MonoBehaviour
{
    [SerializeField] private Button openLeaderboardButton;
    [SerializeField] private string leaderboardSceneName = "Leaderboard";
    [SerializeField] private GameObject asyncLoaderPrefab;


    private void Awake()
    {
        openLeaderboardButton.onClick.AddListener(OpenLeaderboardScene);
    }

    private void OpenLeaderboardScene()
    {
        if (asyncLoaderPrefab != null)
        {
            GameObject loaderGO = Instantiate(asyncLoaderPrefab);
            AsyncSceneLoader loader = loaderGO.GetComponent<AsyncSceneLoader>();
            loader.StartLoadScene(leaderboardSceneName);
        }
        else
        {
            SceneManager.LoadScene(leaderboardSceneName);
        }
    }

}
