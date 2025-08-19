using UnityEngine;

public class IceSlowHandler : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private float slowTimer;
    [SerializeField] private float currentSlowPercent;
    [SerializeField] private float maxSlowPercent;
    [SerializeField] private GameObject vfxInstance;
    [SerializeField] private Transform vfxPoint;

    private float originalSpeed;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement != null)
            originalSpeed = enemyMovement.enemyMoveSpeed;
    }

    private void Update()
    {
        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;

            if (slowTimer <= 0f)
            {
                ResetSlow();
            }
        }
    }

    public void ApplySlow(float baseSlow, float slowIncreasePerTick, float duration, GameObject vfxPrefab)
    {
        // nếu còn hiệu lực → cộng dồn slow
        if (slowTimer > 0f)
        {
            currentSlowPercent += slowIncreasePerTick;
        }
        else
        {
            currentSlowPercent = baseSlow;
        }

        currentSlowPercent = Mathf.Clamp(currentSlowPercent, baseSlow, maxSlowPercent);

        //trừ đi tỉ lệ bị slow để ra tỉ lệ tốc độ hiện tại so với ban đầu
        float slowMultiplier = 1f - currentSlowPercent / 100f;
        enemyMovement.enemyMoveSpeed = originalSpeed * slowMultiplier;

        //đặt lại thời gian slow
        slowTimer = duration;

        // spawn VFX nếu chưa có
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
            vfxInstance.transform.localScale = Vector3.one * height * 0.155f;
        }
    }

    private void ResetSlow()
    {
        currentSlowPercent = 0f;
        enemyMovement.enemyMoveSpeed = originalSpeed;

        if (vfxInstance != null)
        {
            Destroy(vfxInstance);
            vfxInstance = null;
        }
    }
}
