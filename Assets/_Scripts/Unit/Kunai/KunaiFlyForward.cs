using UnityEngine;
using System.Collections;

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
            respawnKunai.NotifyKunaiDestroyed();
            Debug.Log("2 - Cần thêm ObjectPooling ở đây để tránh lỗi null tham chiếu trail khi destroy kunai!");
            Destroy(gameObject);
        }
    }
}
