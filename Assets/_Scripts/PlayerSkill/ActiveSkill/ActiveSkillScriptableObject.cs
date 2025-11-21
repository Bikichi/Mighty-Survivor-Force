using System.Collections.Generic;
using UnityEngine;
//ScriptableObject là một loại asset trong Unity dùng để lưu dữ liệu tách biệt khỏi scene và GameObject
//Lưu data không phụ thuộc vào instance
//Giảm số lượng instance trong RAM → giúp tối ưu game.
[CreateAssetMenu(fileName = "New Active Skill", menuName = "Skill/Active Skill")]
public class ActiveSkillScriptableObject : ScriptableObject
{
    [Header("Thông tin cơ bản")]
    public string skillName;
    public List<string> levelDescriptions = new List<string>();   //mô tả khi nâng cấp
    public Sprite icon;

    [Header("Prefab / Effect")]
    public GameObject effectPrefab;
}
