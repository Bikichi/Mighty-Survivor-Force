using UnityEngine;

public class PlayerShooting : RangedShooterBase
{
    protected override void Start()
    {
        base.Start();
        UpdateShootingRange(); // set lần đầu
        UpdateShootCoolDown();
        PlayerStats.Instance.onAttackRangeChanged += UpdateShootingRange; // đăng ký event
        PlayerStats.Instance.onShootCooldownChanged += UpdateShootCoolDown;

    }

    private void UpdateShootingRange()
    {
        shootingRange = PlayerStats.Instance.baseAttackRange;
    }

    private void UpdateShootCoolDown()
    {
        shootCooldown = PlayerStats.Instance.baseShootCooldown;
    }

    private void OnDestroy()
    {
        PlayerStats.Instance.onAttackRangeChanged -= UpdateShootingRange;
        PlayerStats.Instance.onShootCooldownChanged -= UpdateShootCoolDown;
    }
}
