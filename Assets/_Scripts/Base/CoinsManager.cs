using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CoinManager : Singleton<CoinManager>
{
    public int totalCoinValue = 0;

    private void Awake()
    {
        LoadCoinValue();
    }

    public void LoadCoinValue()
    {
        totalCoinValue = PlayerPrefs.GetInt("totalCoinValue", 0);
    }

    public void SaveCoinValue()
    {
        PlayerPrefs.SetInt("totalCoinValue", totalCoinValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            totalCoinValue += 1000;
            SaveCoinValue();
            Debug.Log("+1000 coins!");
        }
    }
}