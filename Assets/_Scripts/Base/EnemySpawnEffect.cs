using UnityEngine;

public class EnemySpawnEffect : MonoBehaviour
{
    public GameObject effectPrefab;
    public float destroyAfter;

    private void Start()
    {
        TriggerEffect();
    }

    public void TriggerEffect()
    {
        GameObject fx = Instantiate(effectPrefab, transform.position, transform.rotation);
        Destroy(fx, destroyAfter);
    }
}
