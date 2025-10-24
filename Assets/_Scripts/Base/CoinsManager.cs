using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CoinManager : Singleton<CoinManager>
{
    public int totalCoinValue = 0;


    public void Start()
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
}