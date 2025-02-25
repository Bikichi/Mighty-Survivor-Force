using UnityEngine;

[CreateAssetMenu(fileName = "GunnerScriptableObject", menuName = "ScriptableObjects/Gunner")]
public class GunnerScriptableObject : ScriptableObject
{
    //Base stats for the Gunner Player
    public float gunnerMoveSpeed;
    public float gunnerMaxHealth;
    public float gunnerDamage;
}