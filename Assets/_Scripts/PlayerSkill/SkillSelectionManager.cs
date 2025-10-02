using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionManager : MonoBehaviour
{
    [Header("Danh sách tất cả Skill có thể học")]
    [SerializeField] private List<ScriptableObject> allSkills = new List<ScriptableObject>();

    [Header("Kết quả random (Debug hiển thị Inspector)")]
    [SerializeField] private List<ScriptableObject> selectedSkills = new List<ScriptableObject>();

    /// <summary>
    /// Lấy ngẫu nhiên count skill từ danh sách allSkills
    /// </summary>
    public List<ScriptableObject> GetRandomSkillChoices(int count = 3)
    {
        selectedSkills.Clear(); //reset kết quả cũ

        if (allSkills.Count == 0)
        {
            Debug.LogWarning("Chưa có skill nào trong allSkills!");
            return selectedSkills;
        }

        List<ScriptableObject> tempList = new List<ScriptableObject>(allSkills);
        int numberToTake = Mathf.Min(count, tempList.Count);

        for (int i = 0; i < numberToTake; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            selectedSkills.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex); // tránh trùng skill
        }

        return selectedSkills;
    }
}
