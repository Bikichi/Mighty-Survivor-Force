using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSawBladeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _sawBaldeSkill;
    [SerializeField]
    private GameObject[] _sawBaldeModule;
    [SerializeField]
    private int _levelSawBlade = 1;

    void Update()
    {
        //Debug.Log("isLearnSawBladeSkill: " + VariableStatic.isLearnSawBladeSkill);
        CheckLevelSawBalde();
    }

    public void CheckLevelSawBalde()
    {
        if (VariableStatic.isLearnSawBladeSkill == true)
        {
            _sawBaldeSkill.SetActive(true);
            if (_levelSawBlade == 1)
            {
                _sawBaldeModule[0].SetActive(true);
                _sawBaldeModule[1].SetActive(false);
                _sawBaldeModule[2].SetActive(false);
                _sawBaldeModule[3].SetActive(false);
            }
            if (_levelSawBlade == 2)
            {
                _sawBaldeModule[0].SetActive(false);
                _sawBaldeModule[1].SetActive(true);
                _sawBaldeModule[2].SetActive(false);
                _sawBaldeModule[3].SetActive(false);
            }
            if (_levelSawBlade == 3)
            {
                _sawBaldeModule[0].SetActive(false);
                _sawBaldeModule[1].SetActive(false);
                _sawBaldeModule[2].SetActive(true);
                _sawBaldeModule[3].SetActive(false);
            }
            if (_levelSawBlade == 4)
            {
                _sawBaldeModule[0].SetActive(false);
                _sawBaldeModule[1].SetActive(false);
                _sawBaldeModule[2].SetActive(false);
                _sawBaldeModule[3].SetActive(true);
            }
        }
    }
}
