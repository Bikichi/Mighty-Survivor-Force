using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BossChargeSkill : MonoBehaviour, ISkillStatus
{
    [Header("References")]
    public Transform player;
    public GameObject attackPath;
    public GameObject explosionPrefab;

    [Header("Settings")]
    public float windUpTime = 3f;     // Thời gian gồng
    public float chargeSpeed = 10f;   // Tốc độ lao
    public float startTime;
    public float cooldown;
    public float stopDistance = 1f;

    private Rigidbody rb;
    public bool isWindUp = false;
    public bool isCharging = false;
    private bool canSpawnExplosion = true;
    public float windUpTimer = 0f;
    private Vector3 targetPosition;

    public const string preChargeParaname = "PreCharge";
    public const string chargeParaname = "Charge";
    public Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        canSpawnExplosion = true;
        InvokeRepeating("StartCharge", startTime, cooldown);
    }

    private void OnDisable()
    {
        CancelInvoke("StartCharge");
        canSpawnExplosion = false;
        StopCharge();
    }


    private void FixedUpdate()
    {
        if (isWindUp)
        {
            UpdateWindUp();
        }

        if (isCharging)
        {
            UpdateCharge();
        }
    }


    private void BeginWindUp()
    {
        attackPath.SetActive(true);
        anim.SetBool(preChargeParaname, true);
        isWindUp = true;
        windUpTimer = 0f;
        rb.velocity = Vector3.zero;
        //Debug.Log("Boss bắt đầu gồng...");
    }

    private void UpdateWindUp()
    {
        windUpTimer += Time.deltaTime;

        if (windUpTimer >= windUpTime)
        {
            isWindUp = false;
            BeginCharge();
        }
    }

    public void StartCharge()
    {
        if (!isWindUp && !isCharging)
        {
            BeginWindUp();
        }
    }

    public void StopCharge()
    {
        anim.SetBool(chargeParaname, false);
        rb.velocity = Vector3.zero;
        isCharging = false;
        isWindUp = false;
        var bbs = GetComponentInChildren<BossBigStrike>();  
        if (bbs != null)
        {
            bbs.attackTimer = bbs.attackCooldown;
        }
        SpawnExplosion();
    }

    private void BeginCharge()
    {
        attackPath.SetActive(false);
        anim.SetBool(preChargeParaname, false);
        anim.SetBool(chargeParaname, true);
        targetPosition = player.position;

        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * chargeSpeed;

        isCharging = true;
        //Debug.Log("Boss lao tới Player!");
    }

    private void UpdateCharge()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance <= stopDistance)
        {
            StopCharge();
        }
    }

    private void SpawnExplosion()
    {
        if (!canSpawnExplosion) return;
        if (explosionPrefab != null)
        {
            GameObject newObj = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(newObj, 1.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCharging && collision.gameObject.CompareTag(Const.WALL_TAG))
        {
            StopCharge();
        }
    }

    public bool IsActive()
    {
        return isWindUp || isCharging;
    }
}
