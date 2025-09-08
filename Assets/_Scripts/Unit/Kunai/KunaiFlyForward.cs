using UnityEngine;

public class KunaiFlyForward : MonoBehaviour
{
    [SerializeField] private float flySpeed;

    [SerializeField] private KunaiController controller;
    [SerializeField] private RespawnKunai respawnKunai;

    private void Awake()
    {
        controller = GetComponentInParent<KunaiController>();
        respawnKunai = GetComponentInParent<RespawnKunai>();
    }

    private void Update()
    {
        if (controller != null && controller.canFly)
        {
            transform.Translate(Vector3.forward * flySpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.BACKWALL_TAG))
        {
            if (respawnKunai != null)
            {
                respawnKunai.NotifyKunaiDestroyed();
            }
            Destroy(gameObject);
        }
    }
}
