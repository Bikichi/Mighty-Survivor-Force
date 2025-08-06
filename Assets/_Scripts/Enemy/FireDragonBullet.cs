using UnityEngine;

public class FireDragonBullet : BaseBullet
{
    [SerializeField] private GameObject targetPlayer;
    [SerializeField] private Vector3 moveDirection;

    protected override void Start()
    {
        base.Start();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        if (targetPlayer != null)
        {
            Vector3 playerCenter = targetPlayer.transform.position + new Vector3(0, targetPlayer.GetComponent<Collider>().bounds.size.y / 2, 0);
            moveDirection = (playerCenter - transform.position).normalized;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void MoveBullet()
    {
        transform.Translate(moveDirection * _speedBullet * Time.deltaTime, Space.World);
    }


    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.CompareTag(Const.PLAYER_TAG))
        {
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageBullet);
            Destroy(gameObject); ; //hủy viên đạn
        }
    }
}