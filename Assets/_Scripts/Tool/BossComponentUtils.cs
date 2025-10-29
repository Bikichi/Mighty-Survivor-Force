using System.Collections.Generic;
using UnityEngine;

public static class BossComponentUtils
{
    public static void AddBossComponentInChildren<T>(GameObject root, List<MonoBehaviour> list) where T : MonoBehaviour
    {
        if (root == null || list == null) return;

        T comp = root.GetComponentInChildren<T>();
        if (comp != null && !list.Contains(comp))
        {
            list.Add(comp); // T (BossBigStrike) là MonoBehaviour → OK
        }
    }
    public static void AddBossComponentInParent<T>(GameObject root, List<MonoBehaviour> list) where T : MonoBehaviour
    {
        if (root == null || list == null) return;

        T comp = root.GetComponentInParent<T>();
        if (comp != null && !list.Contains(comp))
        {
            list.Add(comp); // T (BossBigStrike) là MonoBehaviour → OK
        }
    }
}
