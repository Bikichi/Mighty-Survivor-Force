using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class MapData
{
    public string mapName;
    public Sprite mapPreview;
    public string sceneName;
    public bool isUnlocked = false;
}

public class MapSelection : MonoBehaviour
{
    public Image previewImage;
    public TMP_Text mapNameText;
    public MapData[] maps;
    public GameObject lockPanel;
    public Button startButton;
    public CanvasGroup startButtonCanvasGroup;
    public PaginationController pagination;

    public GameObject asyncLoaderPrefab;

    private int currentIndex = 0;

    void Start()
    {
        //load trạng thái map mở khóa
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].isUnlocked = MapProgressManager.LoadProgress(i);
        }

        MapSelectionData.maps = maps;
        MapSelectionData.currentIndex = currentIndex;

        UpdateMapDisplay();
    }

    public void NextMap()
    {
        currentIndex = (currentIndex + 1) % maps.Length;
        MapSelectionData.currentIndex = currentIndex;
        AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
        UpdateMapDisplay();
    }

    public void PrevMap()
    {
        currentIndex = (currentIndex - 1 + maps.Length) % maps.Length;
        MapSelectionData.currentIndex = currentIndex;
        AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
        UpdateMapDisplay();
    }

    void UpdateMapDisplay()
    {
        var currentMap = maps[currentIndex];
        mapNameText.text = currentMap.mapName;
        previewImage.sprite = currentMap.mapPreview;
        pagination.SetActiveIndex(currentIndex);

        if (!currentMap.isUnlocked)
        {
            lockPanel.SetActive(true);
            startButton.interactable = false;
            startButtonCanvasGroup.alpha = 0.4f;
        }
        else
        {
            lockPanel.SetActive(false);
            startButton.interactable = true;
            startButtonCanvasGroup.alpha = 1f;
        }
    }

    public void StartGame()
    {
        var currentMap = maps[currentIndex];
        AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
        if (currentMap.isUnlocked)
        {
            GameObject loaderGO = Instantiate(asyncLoaderPrefab);
            AsyncSceneLoader loader = loaderGO.GetComponent<AsyncSceneLoader>();
            loader.StartLoadScene(currentMap.sceneName);
        }
        else
        {
            // fallback
            SceneManager.LoadScene(currentMap.sceneName);
        }
    }
}
