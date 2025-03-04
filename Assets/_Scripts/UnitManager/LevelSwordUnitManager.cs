using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwordUnitManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _swordBaldeSkill;
    [SerializeField]
    private GameObject[] _swordBaldeModule;
    [SerializeField]
    private int _levelSwordBlade = 1;

    void Update()
    {
        CheckLevelSwordBalde();
    }

    public void CheckLevelSwordBalde()
    {
        if (VariableStatic.isLearnSwordBladeSkill == true)
        {
            _swordBaldeSkill.SetActive(true);
            if (_levelSwordBlade == 1)
            {
                _swordBaldeModule[0].SetActive(true);
            }
            if (_levelSwordBlade == 2)
            {
                _swordBaldeModule[0].SetActive(true);
                _swordBaldeModule[1].SetActive(true);
            }
            if (_levelSwordBlade == 3)
            {
                
            }
            if (_levelSwordBlade == 4)
            {
                
            }
        }
    }
}
