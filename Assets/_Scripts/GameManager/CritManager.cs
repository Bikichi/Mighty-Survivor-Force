using UnityEngine;

public class CritManager : Singleton<CritManager>
{
    [Range(0f, 1f)] public float critChance;    
    public float critMultiplier;   

    public (float damage, bool isCrit) CalculateCritDamage(float baseDamage)
    {
        bool isCrit = Random.value < critChance;  //random.value lấy ra giá trị ngẫu nhiên từ 0.0f đến 1.0f
        float finalDamage = isCrit ? baseDamage * critMultiplier : baseDamage;
        return (finalDamage, isCrit);
    }
}
