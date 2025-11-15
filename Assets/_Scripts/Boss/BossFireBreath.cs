using UnityEngine;

public class BossFireBreath : MonoBehaviour, ISkillStatus
{
    [Header("Fire Object")]
    public GameObject fireEffect;        
    public float fireLerpSpeed;        

    //[Header("Scripts to enable during FireBreath")]
    //public MonoBehaviour[] scriptsToEnable; 

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

    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private float originalLerpSpeed;

    private void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        enemyMovement = GetComponent<EnemyMovement>();
        originalLerpSpeed = enemyMovement.lerpSpeed;
        timer = fireCooldown / 2f;
    }

    private void Update()
    {
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
            AudioController.Instance.PlaySoundMultipleTimes(AudioController.Instance.fireBreath, 15, fireDuration / 15);
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
        enemyMovement.lerpSpeed = fireLerpSpeed;

        ////disable các script khác
        //foreach (var script in scriptsToEnable)
        //    script.enabled = false;
    }

    public void StopFire()
    {
        isFiring = false;
        fireEffect.SetActive(false);
        anim.SetBool(fireBoolName, false);
        enemyMovement.lerpSpeed = originalLerpSpeed;
        ////enable các script khác
        //foreach (var script in scriptsToEnable)
        //    script.enabled = true;
    }

    public virtual bool IsActive()
    {
        return isFiring;
    }

}
