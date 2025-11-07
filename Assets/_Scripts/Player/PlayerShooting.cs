using UnityEngine;

public class PlayerShooting : RangedShooterBase
{
    [SerializeField] private PlayerStats playerStats;

    protected override void Start()
    {
        base.Start();
        playerStats = ActivePlayerManager.Instance.CurrentPlayerInstance.GetComponent<PlayerStats>();

        UpdateShootCoolDown();

        // Subscribe sự kiện nếu có thay đổi cooldown
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
}
