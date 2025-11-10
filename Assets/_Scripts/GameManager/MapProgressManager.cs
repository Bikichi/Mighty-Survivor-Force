using Unity.VisualScripting;
using UnityEngine;

public static class MapProgressManager
{
    private const string MapKeyPrefix = "MapUnlocked_";

    /// <summary>
    /// Lưu trạng thái mở khóa của 1 map cụ thể
    /// </summary>
    public static void SaveProgress(int index, bool isUnlocked)
    {
        PlayerPrefs.SetInt(MapKeyPrefix + index, isUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Tải trạng thái mở khóa của 1 map cụ thể
    /// </summary>
    public static bool LoadProgress(int index)
    {
        //map đầu tiên luôn mở mặc định
        if (index == 0)
        {
            PlayerPrefs.SetInt(MapKeyPrefix + 0, 1);
            return true;
        }

        return PlayerPrefs.GetInt(MapKeyPrefix + index, 0) == 1;
    }

    public static void UnlockNextMap()
    {
        if (MapSelectionData.maps == null || MapSelectionData.maps.Length == 0)
            return;

        int nextMapIndex = MapSelectionData.currentIndex + 1;

        // Nếu map tiếp theo tồn tại
        if (nextMapIndex < MapSelectionData.maps.Length)
        {
            MapSelectionData.maps[nextMapIndex].isUnlocked = true;
            SaveProgress(nextMapIndex, true);
        }
    }

    /// <summary>
    /// Xóa toàn bộ dữ liệu map đã lưu
    /// </summary>
    public static void ResetProgress(int totalMaps)
    {
        for (int i = 0; i < totalMaps; i++)
        {
            PlayerPrefs.DeleteKey(MapKeyPrefix + i);
        }
        PlayerPrefs.Save();
    }
}
