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

    //Check Upgrade each every Card
    public static bool[] checkUpgrade = new bool[13];

    //check upgrade and display ui or disable ui
    public static bool displayUIUpgrade = true;

    //
    public static bool isBuildForceField = false;

    //Check main raft Player;
    public static bool isMainRaft = true;

    private void Update()
    {
        if (LevelManager.isUpgrade == false)
        {
            VariableStatic.displayUIUpgrade = false;// disable ui upgrade
        }
    }
}
