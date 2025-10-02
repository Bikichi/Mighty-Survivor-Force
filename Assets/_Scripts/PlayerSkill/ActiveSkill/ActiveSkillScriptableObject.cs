using UnityEngine;

[CreateAssetMenu(fileName = "New Active Skill", menuName = "Skill/Active Skill")]
public class ActiveSkillScriptableObject : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite icon;
    
    public GameObject prefab;
}