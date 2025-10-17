using UnityEngine;

public class BossAttackPath : MonoBehaviour
{
    [Header("References")]
    public Transform player;       
    public Transform attackPath;   

    [Header("Settings")]
    public float scaleMultiplier;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        attackPath.rotation = Quaternion.LookRotation(direction);

        //đặt tâm AttackPath ở giữa Boss và Player
        attackPath.position = transform.position + direction * 0.5f;

        //kéo dài thanh theo trục Z
        Vector3 scale = attackPath.localScale;
        scale.z = distance * scaleMultiplier;
        attackPath.localScale = scale;
    }
}
