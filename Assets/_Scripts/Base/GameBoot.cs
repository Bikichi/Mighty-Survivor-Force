using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBoot : MonoBehaviour
{
    public GameObject asyncLoaderPrefab;
    public string firstScene;

    void Awake()
    {
        if (asyncLoaderPrefab != null)
        {
            GameObject loaderGO = Instantiate(asyncLoaderPrefab);
            AsyncSceneLoader loader = loaderGO.GetComponent<AsyncSceneLoader>();
            loader.StartLoadScene(firstScene);
        }
        else
        {
            SceneManager.LoadScene("_MainMenuScene");
        }
    }
}
