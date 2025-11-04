using System.Collections.Generic;
using UnityEngine;

public class CheckAlertUIManager : Singleton<CheckAlertUIManager>
{
    [SerializeField] private List<GameObject> waveAlertUIs = new List<GameObject>();
    public bool IsAnyWaveAlertActive()
    {
        foreach (var ui in waveAlertUIs)
        {
            if (ui != null && ui.activeSelf)
                return true;
        }
        return false;
    }
}
