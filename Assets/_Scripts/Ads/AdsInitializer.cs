using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    // Trạng thái NoAds
    [SerializeField] public bool isNoAds = false;

    public event Action OnNoAdsPurchased;
    public static AdsInitializer Instance { get; private set; }

    void Awake()
    {
        //đảm bảo chỉ có 1 AdsInitializer duy nhất
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadNoAdsState();     //Load trạng thái NoAds trước

        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!isNoAds)  //Chỉ init Ads nếu chưa mua NoAds
        {
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }
    }

    public void BuyNoAds()
    {
        isNoAds = true;
        PlayerPrefs.SetInt("NoAds", 1);
        PlayerPrefs.Save();

        OnNoAdsPurchased?.Invoke();

        Debug.Log("No Ads purchased. Ads disabled.");
    }

    private void LoadNoAdsState()
    {
        isNoAds = PlayerPrefs.GetInt("NoAds", 0) == 1;
        //Debug.Log("Load NoAds State: " + isNoAds);
    }

    public void ResetNoAds()
    {
        PlayerPrefs.DeleteKey("NoAds");
        PlayerPrefs.Save();

        isNoAds = false;

        Debug.Log("NoAds reset. Ads enabled again.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ResetNoAds();
            Debug.Log("Pressed A → ResetNoAds()");
        }
    }

    public void OnInitializationComplete()
    {
        //Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
