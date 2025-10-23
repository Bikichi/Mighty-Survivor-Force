using UnityEngine;

/// <summary>
/// Đạn enemy bay theo đường vòng cung (parabola), không tracking.
/// Dành cho boss hoặc enemy có kiểu bắn ném.
/// </summary>
public class EnemyArcBullet : BaseBullet
{
    [SerializeField] private GameObject targetPlayer;
    [SerializeField] private float arcHeight = 3f;   // độ cao của đường cong
    [SerializeField] private float flightTime = 1.5f; // thời gian bay từ đầu đến đích

    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer;

    protected override void Start()
    {
        base.Start();

        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;

        // Lấy vị trí trung tâm player (không check null để dễ debug)
        Collider playerCol = targetPlayer.GetComponent<Collider>();
        Vector3 playerCenter = targetPlayer.transform.position + new Vector3(0, playerCol.bounds.size.y / 2, 0);
        targetPos = playerCenter;
    }

    protected override void MoveBullet()
    {
        timer += Time.deltaTime;
        float progress = timer / flightTime;

        if (progress >= 1f)
        {
            Destroy(gameObject);
            return;
        }

        // Di chuyển theo đường cong (parabola)
        Vector3 nextPos = Vector3.Lerp(startPos, targetPos, progress);
        float height = Mathf.Sin(progress * Mathf.PI) * arcHeight;
        nextPos.y += height;

        // Cập nhật vị trí
        transform.position = nextPos;

        // Xoay theo hướng bay
        Vector3 dir = nextPos - transform.position;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);

        if (col.CompareTag(Const.PLAYER_TAG))
        {
            // Gây damage thẳng, không kiểm tra null
            col.GetComponent<PlayerHealth>().TakeDamage(damageBullet);
            Destroy(gameObject);
        }
        else if (col.CompareTag(Const.WALL_TAG) || col.CompareTag(Const.PLANE_TAG))
        {
            Destroy(gameObject);
        }
    }
}
