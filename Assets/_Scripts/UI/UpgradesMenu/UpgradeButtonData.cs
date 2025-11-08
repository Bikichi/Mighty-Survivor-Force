using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonData : MonoBehaviour
{
    public enum UpgradeType
    {
        Health,
        Damage,
        Speed,
        Cooldown
    }

    [Header("Upgrade Info")]
    public int cost;
    public float value;
    public UpgradeType type;
    
    [Header("Core")]
    public Button button;

    [Header("UI")]
    public Sprite icon; 

    [Header("Purchased Visual")]
    public GameObject purchasedBG;

    [HideInInspector] public bool isPurchased = false;
}
