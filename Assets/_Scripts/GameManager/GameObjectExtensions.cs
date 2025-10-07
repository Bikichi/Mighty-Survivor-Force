using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// Bật hoặc tắt tất cả GameObject con (không bao gồm chính đối tượng này).
    /// </summary>
    public static void SetChildrenActive(this GameObject parent, bool active)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
