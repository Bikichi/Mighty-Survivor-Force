using UnityEngine;

/// <summary>
/// Đạn của Boss bay theo đường vòng cung tới vị trí ngẫu nhiên
/// trong một vùng tấn công cố định trên bản đồ (tọa độ cố định, không quanh player).
/// </summary>
public class BossArcBullet : EnemyArcBullet
{
    [Header("Fixed Attack Area Settings")]
    [SerializeField] private Vector3 areaCenter = new Vector3(0.02f, 1.58f, -2.5f); // tọa độ trung tâm vùng tấn công cố định
    [SerializeField] private Vector3 areaSize = new Vector3(10f, 0f, 10f); // kích thước vùng tấn công

    protected override void Start()
    {
        base.Start();

        // Random vị trí trong vùng tấn công cố định (theo world space)
        Vector3 randomOffset = new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2),
            Random.Range(-areaSize.z / 2, areaSize.z / 2)
        );

        targetPos = areaCenter + randomOffset;

        // Nếu vùng phẳng (Y = 0), giữ nguyên chiều cao viên đạn
        if (Mathf.Approximately(areaSize.y, 0f))
            targetPos.y = transform.position.y;
    }

    // Vẽ vùng tấn công trong Scene view để dễ thấy
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        Gizmos.DrawCube(areaCenter, areaSize);
    }
}
