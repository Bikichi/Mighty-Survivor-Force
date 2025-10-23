using UnityEngine;

public class BossFireBreath : MonoBehaviour, ISkillStatus
{
    [Header("Fire Object")]
    public GameObject fireEffect;        
    public float lerpSpeed;        

    [Header("Scripts to enable during FireBreath")]
    public MonoBehaviour[] scriptsToEnable; 

    [Header("Target")]
    [SerializeField] public GameObject targetPlayer;

    [Header("Fire Settings")]
    public float fireDuration = 3f;
    public float fireCooldown = 5f;

    [Header("Animation")]
    public Animator anim;                  
    public string fireBoolName = "isFiring"; 

    private bool isFiring = false;
    private float timer;

    private void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        timer = fireCooldown / 2f;
    }

    private void Update()
    {
        if (isFiring)
        {
            RotateTowardsPlayer();
        }

        timer += Time.deltaTime;

        if (isFiring)
        {
            HandleFiring();
        }
        else
        {
            HandleCooldown();
        }
    }

    private void HandleFiring()
    {
        if (timer >= fireDuration)
        {
            StopFire();
            timer = 0f; //reset timer để bắt đầu cooldown
        }
    }

    private void HandleCooldown()
    {
        if (timer >= fireCooldown)
        {
            StartFire();
            timer = 0f; //reset timer khi bắt đầu Fire
        }
    }


    private void StartFire()
    {
        isFiring = true;
        fireEffect.SetActive(true);
        anim.SetBool(fireBoolName, true);

        //disable các script khác
        foreach (var script in scriptsToEnable)
            script.enabled = false;
    }

    private void StopFire()
    {
        isFiring = false;
        fireEffect.SetActive(false);
        anim.SetBool(fireBoolName, false);

        //enable các script khác
        foreach (var script in scriptsToEnable)
            script.enabled = true;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lerpSpeed * Time.deltaTime);
    }

    public virtual bool IsActive()
    {
        return isFiring;
    }

}
