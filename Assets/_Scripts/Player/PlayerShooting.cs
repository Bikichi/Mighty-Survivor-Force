using UnityEngine;
public enum WeaponType
{
    Gun,
    Bow
}
public class PlayerShooting : RangedShooterBase
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private WeaponType weaponType; // ✅ Loại vũ khí của player này

    protected override void Start()
    {
        base.Start();
        playerStats = ActivePlayerManager.Instance.CurrentPlayerInstance.GetComponent<PlayerStats>();
        UpdateShootCoolDown();

        //subscribe sự kiện nếu có thay đổi cooldown
        playerStats.onShootCooldownChanged += UpdateShootCoolDown;
    }

    private void UpdateShootCoolDown()
    {
        if (playerStats != null)
            shootCooldown = playerStats.baseShootCooldown;
    }

    private void OnDestroy()
    {
        if (playerStats != null)
            playerStats.onShootCooldownChanged -= UpdateShootCoolDown;
    }

    // 🔫 Gọi hàm này khi bắn (ví dụ trong Fire() hoặc Shoot())
    protected override void ShootBullet()
    {
        base.ShootBullet();
        PlayShootSound();
    }

    private void PlayShootSound()
    {
        switch (weaponType)
        {
            case WeaponType.Gun:
                AudioController.Instance.PlaySound(AudioController.Instance.chesterShot);
                break;
            case WeaponType.Bow:
                AudioController.Instance.PlaySound(AudioController.Instance.archerShot);
                break;
        }
    }
}
