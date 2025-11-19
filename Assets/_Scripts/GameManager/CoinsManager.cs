using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CoinManager : Singleton<CoinManager>
{
    public int totalCoinValue = 0;
    public int inGameCoin;

    private void Awake()
    {
        LoadCoinValue();
        ResetInGameCoin();
    }
    public void ResetInGameCoin()
    {
        inGameCoin = 0;
    }
    public void LoadCoinValue()
    {
        totalCoinValue = PlayerPrefs.GetInt("totalCoinValue", 0);
    }

    public void SaveCoinValue()
    {
        PlayerPrefs.SetInt("totalCoinValue", totalCoinValue);
    }
    public void ResetAllData()
    {
        totalCoinValue = 0;

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs cleared. Coin reset to 0.");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetAllData();
        }
    }
}