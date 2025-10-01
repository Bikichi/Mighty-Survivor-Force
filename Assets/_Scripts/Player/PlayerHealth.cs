using UnityEngine;

public class PlayerHealth : LivingEntity
{
    [Header("Dodge Settings")]
    [SerializeField] private GameObject missTextPrefab;
    [SerializeField] private Transform uiParent;

    protected override void Awake()
    {
        UpdateMaxHealth(); //set lần đầu
        base.Awake();
        PlayerStats.Instance.onMaxHealthChanged += UpdateMaxHealth; //đăng ký sự kiện
    }

    private void UpdateMaxHealth()
    {
        //tính phần chênh lệch maxHP cũ và mới trước ghi gán
        float diff = PlayerStats.Instance.maxHP - maxHealth;
        
        maxHealth = PlayerStats.Instance.maxHP; //gán

        //tăng currentHealth tương ứng chênh lệch, không vượt maxHealth vừa gán
        currentHealth = Mathf.Min(currentHealth + diff / 2, maxHealth);

        onHealthChange?.Invoke(currentHealth, maxHealth); // update HealthBar
    }

    public override void TakeDamage(float damage)
    {
        //cập nhật tỉ lệ né trực tiếp từ PlayerStats mỗi lần TakeDamage
        float dodgeChance = PlayerStats.Instance.baseDodgeChance / 100f; //chia 100 để dùng với Random.value (0-1)
        if (Random.value < dodgeChance)
        {
            ShowMissText();
            return;
        }
        base.TakeDamage(damage);
    }

    private void ShowMissText()
    {
        GameObject missObj = Instantiate(missTextPrefab, uiParent);
        Collider colPlayer = GetComponent<Collider>();
        missObj.transform.position = colPlayer.bounds.center + new Vector3(0, colPlayer.bounds.size.y * 0.6f, 0);
        missObj.GetComponent<FloatingText>().Setup("Dodge", Color.yellow);
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Player Die!!!");
    }

    private void OnDestroy()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.onMaxHealthChanged -= UpdateMaxHealth; // hủy đăng ký tránh memory leak
    }
}
