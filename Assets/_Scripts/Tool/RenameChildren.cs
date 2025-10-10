using UnityEngine;
using UnityEditor;

public class RenameChildren : MonoBehaviour
{
    [MenuItem("Tools/Rename Children Sequentially")]
    private static void RenameSelectedChildren()
    {
        GameObject parent = Selection.activeGameObject;

        if (parent == null)
        {
            Debug.LogWarning("Vui lòng chọn GameObject cha (ví dụ: EnemySpawner).");
            return;
        }

        int count = 1;
        foreach (Transform child in parent.transform)
        {
            Undo.RecordObject(child.gameObject, "Rename Child");
            child.name = $"SpawnPoint {count}";
            count++;
        }

        Debug.Log($"Đã đổi tên {count - 1} object con của {parent.name}");
    }
}
