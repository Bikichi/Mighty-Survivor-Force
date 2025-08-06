using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableStatic : MonoBehaviour
{
    public static bool panelUpgradeIsShow = true;
    public static GameObject _unitPrefab;
    public static bool chooseCard;
    public static bool isClicked = false;
    public static List<int> numberCard = new List<int>();
    public static bool CanChoose;

    //Upgrade unit lv1 -> lv4
    public static int _currentLevel = 1;

    ////Kiểm tra xem trong tất cả 10 skill có trong game, skill nào đã được học
    //public static bool[] checkUpgrade = new bool[10];

    //check upgrade and display ui or disable ui
    public static bool displayUIUpgrade = true;

    public static bool isLearnSawBladeSkill = false;
    public static bool isLearnSwordBladeSkill = false;
    public static bool isLearnMineSkill = false;

    private void Update()
    {
        if (LevelManager.isUpgrade == false)
        {
            VariableStatic.displayUIUpgrade = false;// disable ui upgrade
        }
    }
}
