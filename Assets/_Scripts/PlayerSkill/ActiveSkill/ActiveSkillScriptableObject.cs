using System.Collections.Generic;
using UnityEngine;

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
