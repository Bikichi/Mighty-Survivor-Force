using UnityEngine;

public class PlayerPowerUPEffect : MonoBehaviour
{
    public GameObject effectPrefab;
    public float destroyAfter;
    public float yOffset = 1f;   // khoảng offset theo trục Y

    public void TriggerEffect()
    {
        if (effectPrefab == null) return;
        AudioController.Instance.PlaySound(AudioController.Instance.playerLVLUP);

        Vector3 spawnPos = new Vector3(
            transform.position.x,
            transform.position.y + yOffset,
            transform.position.z
        );

        GameObject fx = Instantiate(
            effectPrefab,
            spawnPos,
            transform.rotation,
            transform
        );

        Destroy(fx, destroyAfter);
    }
}
