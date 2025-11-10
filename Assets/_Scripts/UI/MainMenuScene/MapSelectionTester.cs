using UnityEngine;

public class MapSelectionTester : MonoBehaviour
{
    public MapSelection mapSelection; // Gắn trong Inspector

    void Update()
    {
        // Nhấn R để reset tiến độ
        if (Input.GetKeyDown(KeyCode.R))
        {
            MapProgressManager.ResetProgress(mapSelection.maps.Length);
  
            Debug.Log("Map Progress reset! Tắt đi bật lại PLAYMODE để load lại");
        }
    }
}
