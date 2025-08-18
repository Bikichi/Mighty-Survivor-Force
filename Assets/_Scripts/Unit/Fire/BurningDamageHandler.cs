using UnityEngine;

public class BurningDamageHandler : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private float burnTimer; //thời gian cháy còn lại 
    [SerializeField] private float currentDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private GameObject vfxInstance;    
    [SerializeField] private Transform vfxPoint;       

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        //nếu còn thời gian cháy → giảm dần
        if (burnTimer > 0f)
        {
            burnTimer -= Time.deltaTime;

            //nếu vừa hết cháy → xóa VFX
            if (burnTimer <= 0f && vfxInstance != null)
            {
                Destroy(vfxInstance);
                vfxInstance = null;
            }
        }
    }

    public void ApplyBurn(float baseDamage, float damageIncreasePerTick, float duration, GameObject vfxPrefab)
    {
        //nếu vẫn đang cháy → tăng damage
        if (burnTimer > 0f)
        {
            currentDamage = Mathf.Min(currentDamage + damageIncreasePerTick, maxDamage);
        }
        else
        {
            currentDamage = baseDamage;
        }

        enemyHealth.TakeDamage(currentDamage);

        //làm mới thời gian cháy
        burnTimer = duration;

        //spawn VFX nếu chưa có
        if (vfxPrefab != null && vfxInstance == null)
        {
            vfxInstance = Instantiate(vfxPrefab, vfxPoint.position, Quaternion.identity, transform);

            //lấy chiều cao enemy
            float height = 1f;
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                height = col.bounds.size.y;
            }

            //scale theo chiều cao enemy
            vfxInstance.transform.localScale = Vector3.one * height * 0.125f;
        }
    }
}
