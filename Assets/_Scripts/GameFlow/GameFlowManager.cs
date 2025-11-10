using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
    public void OnAllWavesCompleted()
    {
        MapProgressManager.UnlockNextMap();
        FindAnyObjectByType<UIManager>().ShowGameComplete();
    }
}
