using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BossChargeSkill : MonoBehaviour, ISkillStatus
{
    [Header("References")]
    public Transform player;
    public GameObject attackPath;

    [Header("Settings")]
    public float windUpTime = 3f;     // Thời gian gồng
    public float chargeSpeed = 10f;   // Tốc độ lao
    public float startTime;
    public float cooldown;
    public float stopDistance = 1f;

    private Rigidbody rb;
    public bool isWindUp = false;
    public bool isCharging = false;
    public float windUpTimer = 0f;
    private Vector3 targetPosition;

    public const string preChargeParaname = "PreCharge";
    public const string chargeParaname = "Charge";
    public Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        InvokeRepeating("StartCharge", startTime, cooldown);
    }

    private void OnDisable()
    {
        CancelInvoke("StartCharge");

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
        rb.velocity = Vector3.zero;
        isCharging = false;
        isWindUp = false;
        var bbs = GetComponentInChildren<BossBigStrike>();  
        if (bbs != null)
        {
            bbs.attackTimer = bbs.attackCooldown;
        }
        //Debug.Log("Boss kết thúc lao!");
    }

    private void BeginCharge()
    {
        attackPath.SetActive(false);
        anim.SetBool(preChargeParaname, false);
        anim.SetTrigger(chargeParaname);
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
