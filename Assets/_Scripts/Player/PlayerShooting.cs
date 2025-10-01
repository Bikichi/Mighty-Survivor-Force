using UnityEngine;

public class PlayerShooting : RangedShooterBase
{
    protected override void Start()
    {
        base.Start();

        UpdateShootCoolDown();
        PlayerStats.Instance.onShootCooldownChanged += UpdateShootCoolDown;

    }

    private void UpdateShootCoolDown()
    {
        shootCooldown = PlayerStats.Instance.baseShootCooldown;
    }

    private void OnDestroy()
    {
        PlayerStats.Instance.onShootCooldownChanged -= UpdateShootCoolDown;
    }
}
