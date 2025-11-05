using UnityEngine;

public class MapSelectionTester : MonoBehaviour
{
    public MapSelection mapSelection; // Gắn trong Inspector

    void Update()
    {
        // Nhấn U để mở khóa map kế tiếp
        if (Input.GetKeyDown(KeyCode.U))
        {
            mapSelection.UnlockNextMap();
            Debug.Log("Unlocked next map!");
        }

        // Nhấn R để reset tiến độ
        if (Input.GetKeyDown(KeyCode.R))
        {
            mapSelection.ResetProgress();
            Debug.Log("Progress reset!");
        }
    }
}
