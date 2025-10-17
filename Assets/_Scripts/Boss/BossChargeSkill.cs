using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BossChargeSkill : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject attackPath;

    [Header("Settings")]
    public float windUpTime = 3f;     // Thời gian gồng
    public float chargeSpeed = 10f;   // Tốc độ lao
    public float startTime;
    public float cooldown;

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

    private void Update()
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
        Vector3 toTarget = targetPosition - transform.position;
        Vector3 direction = rb.velocity.normalized;

        // Nếu dot <= 0, nghĩa là Boss đã vượt target
        if (Vector3.Dot(toTarget, direction) <= 0f)
        {
            StopCharge();
        }
    }
}
