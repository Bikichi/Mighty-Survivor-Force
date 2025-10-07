using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillModule
{
    public string skillName;           //tên skill
    public GameObject skillParent;     //parent GameObject của skill
    public GameObject[] modulePrefabs;       //các module tương ứng với từng level
}

public class SkillModuleManager : MonoBehaviour
{
    [SerializeField] private PlayerSkillManager playerSkillManager;
    [SerializeField] public SkillModule[] skillModules; //danh sách các skill cần quản lý

    public KunaiController kunaiController;

    public void UpdateModules(SkillModule skillModule, int skillLevel)
    {
        //bật parent
        skillModule.skillParent.SetActive(true);

        //bật module tương ứng, tắt các module khác
        for (int i = 0; i < skillModule.modulePrefabs.Length; i++)
        {
            skillModule.modulePrefabs[i].SetActive(i == skillLevel - 1);
        }


        if (skillModule.skillName.ToLower().Contains("kunai"))
        {
            kunaiController.ResetState();
            kunaiController.ResetCoroutines();
        }
    }
}
