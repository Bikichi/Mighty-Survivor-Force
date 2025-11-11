using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class PlayerHealth : LivingEntity
{
    public UnityEvent onTakeDamage;

    [Header("Dodge Settings")]
    [SerializeField] private GameObject missTextPrefab;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] protected List<MonoBehaviour> componentsToDisable;
    [SerializeField] private Animator anim;           
    [SerializeField] private float deathAnimationTime = 1f;

    protected override void Awake()
    {
        //// Lấy PlayerStats trực tiếp từ CurrentPlayerInstance
        //playerStats = ActivePlayerManager.CurrentPlayerInstance.GetComponent<PlayerStats>();
        base.Awake();
    }
    private void Start()
    {
        playerStats = ActivePlayerManager.Instance.CurrentPlayerInstance.GetComponent<PlayerStats>();
        playerStats.onMaxHealthChanged += UpdateMaxHealth; // đăng ký sự kiện
        maxHealth = playerStats.maxHP;
        currentHealth = maxHealth;
    }

    private void UpdateMaxHealth()
    {
        // tính phần chênh lệch maxHP cũ và mới trước khi gán
        float diff = playerStats.maxHP - maxHealth;

        maxHealth = playerStats.maxHP; // gán

        // tăng currentHealth tương ứng chênh lệch, không vượt maxHealth
        currentHealth = Mathf.Min(currentHealth + diff / 2, maxHealth);

        onHealthChange?.Invoke(currentHealth, maxHealth); // update HealthBar
    }

    public override void TakeDamage(float damage, bool isCrit = false)
    {
        // cập nhật tỉ lệ né trực tiếp từ PlayerStats
        float dodgeChance = playerStats.baseDodgeChance / 100f;
        if (Random.value < dodgeChance)
        {
            ShowMissText();
            return;
        }

        base.TakeDamage(damage);
        onTakeDamage?.Invoke();
    }

    public void TakeDamageFromBeam(float damage)
    {
        base.TakeDamage(damage);
        onTakeDamage?.Invoke();
    }

    private void ShowMissText()
    {
        GameObject missObj = Instantiate(missTextPrefab);
        Collider colPlayer = GetComponent<Collider>();
        missObj.transform.position = colPlayer.bounds.center + new Vector3(0, colPlayer.bounds.size.y * 0.6f, 0);
        missObj.GetComponent<FloatingText>().Setup("Dodge", Color.yellow);
    }

    protected override void Die()
    {
        IsActive = false;
        IsDead = true;
        DisableEnemyActions();
        anim.SetTrigger("Dead");
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathAnimationTime);

        onDeath?.Invoke();
    }
    private void DisableEnemyActions()
    {
        foreach (var comp in componentsToDisable)
        {
            comp.enabled = false;
        }
    }

    private void OnDestroy()
    {
        playerStats.onMaxHealthChanged -= UpdateMaxHealth; // hủy đăng ký tránh memory leak
    }
}
