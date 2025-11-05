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

    private int currentIndex = 0;

    void Start()
    {
        //load trạng thái map mở khóa
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].isUnlocked = MapProgressManager.LoadProgress(i);
        }

        UpdateMapDisplay();
    }

    public void NextMap()
    {
        currentIndex = (currentIndex + 1) % maps.Length;
        UpdateMapDisplay();
    }

    public void PrevMap()
    {
        currentIndex = (currentIndex - 1 + maps.Length) % maps.Length;
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
        if (currentMap.isUnlocked)
        {
            SceneManager.LoadScene(currentMap.sceneName);
        }
    }

    //gọi khi người chơi thắng level hiện tại
    public void UnlockNextMap()
    {
        int nextIndex = currentIndex + 1;

        if (nextIndex < maps.Length)
        {
            maps[nextIndex].isUnlocked = true;
            MapProgressManager.SaveProgress(nextIndex, true); //lưu duy nhất map vừa mở khóa
        }
    }

    //xóa toàn bộ dữ liệu
    public void ResetProgress()
    {
        MapProgressManager.ResetProgress(maps.Length);
        UpdateMapDisplay();
    }
}
