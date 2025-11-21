using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionManager : MonoBehaviour
{
    [Header("Player Skill Manager")]
    [SerializeField] private PlayerSkillManager playerSkillManager;

    [Header("Danh sách tất cả Skill có thể học")]
    [SerializeField] private List<ScriptableObject> allSkills = new List<ScriptableObject>();

    [Header("Kết quả random (Debug hiển thị Inspector)")]
    [SerializeField] private List<ScriptableObject> selectedSkills = new List<ScriptableObject>();

    /// <summary>
    /// Lấy ngẫu nhiên count skill từ danh sách phù hợp
    /// </summary>
    public List<ScriptableObject> GetRandomSkillChoices(int count = 3)
    {
        selectedSkills.Clear();

        List<ScriptableObject> pool = new List<ScriptableObject>();

        if (playerSkillManager.LearnedSkillCount == 5)
        {
            //lấy các skill đã học nhưng chưa max level 4
            pool = playerSkillManager.LearnedSkills
                .FindAll(entry => entry.level < 4)
                .ConvertAll(entry => entry.skill); //ConvertAll chuyển đổi phần tử sang dạng khác và thêm vào 1 list mới
        }
        else if (playerSkillManager.LearnedSkillCount < 5)
        {
            //lấy các skill trong allSkills mà chưa max level
            foreach (var skill in allSkills)
            {
                int lvl = playerSkillManager != null ? playerSkillManager.GetSkillLevel(skill) : 0;
                if (lvl < 4)
                    pool.Add(skill);
            }
        }

        if (pool.Count == 0) //không còn skill nào để học
            return selectedSkills;

        int numberToTake = Mathf.Min(count, pool.Count);

        for (int i = 0; i < numberToTake; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);
            selectedSkills.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex); //tránh trùng trong lần random tiếp
        }

        return selectedSkills;
    }
}
