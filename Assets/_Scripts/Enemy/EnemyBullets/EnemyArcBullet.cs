using UnityEngine;

/// <summary>
/// Đạn enemy bay theo đường vòng cung (parabola), không tracking.
/// Dành cho boss hoặc enemy có kiểu bắn ném.
/// </summary>
public class EnemyArcBullet : BaseBullet
{
    [SerializeField] private GameObject targetPlayer;
    [SerializeField] private float arcHeight = 10f;    // độ cao của đường cong
    [SerializeField] private float lerpSpeed = 3f;    // tốc độ bay (lerp speed)
    [SerializeField] private float lifetime = 10f;     // thời gian tồn tại trước khi tự hủy

    private Vector3 startPos;
    protected Vector3 targetPos;
    private float lerpT;   // giá trị tiến trình di chuyển (0 → 1)
    private float lifeTimer;

    protected override void Start()
    {
        base.Start();

        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;

        targetPos = targetPlayer.transform.position;
    }

    protected override void MoveBullet()
    {
        // Tăng thời gian sống
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifetime)
        {
            Destroy(gameObject);
            return;
        }

        // Tăng giá trị lerp
        lerpT += Time.deltaTime * lerpSpeed;
        lerpT = Mathf.Clamp01(lerpT);

        // Tính vị trí tiếp theo theo đường cong parabola
        Vector3 nextPos = Vector3.Lerp(startPos, targetPos, lerpT);
        float height = Mathf.Sin(lerpT * Mathf.PI) * arcHeight;
        nextPos.y += height;

        // Cập nhật vị trí
        transform.position = nextPos;

        // Xoay theo hướng bay
        Vector3 dir = targetPos - transform.position;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.CompareTag(Const.WALL_TAG) || col.CompareTag(Const.PLANE_TAG))
        {
            //do something
        }
    }
}
