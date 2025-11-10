using UnityEngine;
using UnityEngine.UI;

public class UpgradeLineConnector : MonoBehaviour
{
    public Image fillImage; //dạng filled
    public float fillValue = -0.1f;

    private string FillKey => $"LineConnector_{gameObject.name}_FillValue";

    private void Start()
    {
        LoadFillValue();
        ApplyFill();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetFill();
            Debug.Log("Tất cả fill buttons đẫ được reset!");
        }
    }

    public void AddFill(float amount)
    {
        fillValue = Mathf.Clamp01(fillValue + amount);
        ApplyFill();
        SaveFillValue();
    }

    private void ApplyFill()
    {
        fillImage.fillAmount = fillValue;
    }

    public void SaveFillValue()
    {
        PlayerPrefs.SetFloat(FillKey, fillValue);
        PlayerPrefs.Save();
    }

    public void LoadFillValue()
    {
        if (PlayerPrefs.HasKey(FillKey))
        {
            fillValue = PlayerPrefs.GetFloat(FillKey);
        }
        else
        {
            fillValue = -0.1f;   // dùng default bạn muốn
        }
    }

    public void ResetFill()
    {
        fillValue = -0.1f;
        PlayerPrefs.DeleteKey(FillKey);
        PlayerPrefs.Save();
        ApplyFill();
    }
}
