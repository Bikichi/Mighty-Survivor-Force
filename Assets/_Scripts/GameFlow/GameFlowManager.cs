using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
    public void OnAllWavesCompleted()
    {
        MapSelection.Instance.UnlockNextMap();
    }
}
