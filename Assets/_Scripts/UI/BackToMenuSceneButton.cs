using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuSceneButton : MonoBehaviour
{
    public GameObject asyncLoaderPrefab;
    public void BackToMenu()
    {
        if (asyncLoaderPrefab != null)
        {
            AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
            GameObject loaderGO = Instantiate(asyncLoaderPrefab);
            AsyncSceneLoader loader = loaderGO.GetComponent<AsyncSceneLoader>();
            loader.StartLoadScene("_MainMenuScene");
        }
        else
        {
            AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
            SceneManager.LoadScene("_MainMenuScene");
        }

    }
}
