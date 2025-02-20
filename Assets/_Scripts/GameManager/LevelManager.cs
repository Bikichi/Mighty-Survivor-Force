using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Slider _levelBar;
    [SerializeField] private Text _levelText;

    [SerializeField] private float _currentLevel = 1;
    [SerializeField] public float _pointExperience = 0;
    [SerializeField] private float _maxLevelUpExperience = 100;
    [SerializeField] public float _plusExperiencePoint;
    [SerializeField] private float _experienceIncrease = 1.1f;

    [SerializeField] private GameObject _levelUI;

    public static bool levelUpUI;

    public static bool isUpgrade;

    public static bool IsExperienceMove = true;
    public static float increaseExperience = 1f;

    private void Update()
    {
        _levelBar.value = (float)_pointExperience / _maxLevelUpExperience;
        
        if (_pointExperience >= _maxLevelUpExperience)
        {
            _levelBar.value = 0;
            _pointExperience = 0;
            _currentLevel++;
            _maxLevelUpExperience *= _experienceIncrease;
            _levelText.text = "" + _currentLevel;
            LevelUpUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Experience"))
        {
            _pointExperience += _plusExperiencePoint;
        }
    }

    public void LevelUpUI()
    {
        VariableStatic.panelUpgradeIsShow = true;
        VariableStatic.chooseCard = true;
        levelUpUI = true;
    }
}
