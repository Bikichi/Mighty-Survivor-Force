using UnityEngine;

[CreateAssetMenu(fileName = "New Active Skill", menuName = "Skill/Active Skill")]
public class ActiveSkillScriptableObject : ScriptableObject
{
    [Header("Thông tin cơ bản")]
    public string skillName;
    [TextArea] public string description;          //mô tả lần đầu học
    [TextArea] public string upgradeDescription;   //mô tả khi nâng cấp
    public Sprite icon;

    [Header("Prefab / Effect")]
    public GameObject prefab;
}
