using UnityEngine;
using System.Collections;

public class RespawnKunai : MonoBehaviour
{
    [SerializeField] private GameObject kunaiGroupPrefab;
    [SerializeField] private string prefabPath;
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private int totalKunai;
    [SerializeField] private int destroyedKunai;
    [SerializeField] private int currentLevel;

    private void Awake()
    {
        //currentLevel = ...; // sau này làm quản lý lv sẽ thêm logic check lv
        prefabPath = $"Prefabs/Unit/Kunai/KunaiLv{currentLevel}";
        kunaiGroupPrefab = Resources.Load<GameObject>(prefabPath);
    }

    private void Start()
    {
        totalKunai = GetComponentsInChildren<KunaiFlyForward>().Length;
        destroyedKunai = 0;
    }

    public void NotifyKunaiDestroyed()
    {
        destroyedKunai++;

        if (destroyedKunai >= totalKunai)
        {
            StartCoroutine(RespawnKunaiGroup());
        }
    }

    private IEnumerator RespawnKunaiGroup()
    {
        yield return new WaitForSeconds(respawnDelay);

        Transform kunaiUnitParent = transform.parent;
        KunaiController controller = GetComponentInParent<KunaiController>();
        
        controller.ResetState();

        //spawn group mới làm con của KunaiUnit
        GameObject newGroup = Instantiate(kunaiGroupPrefab, kunaiUnitParent);
        
        controller.ResetCoroutines();

        //hủy group cũ (trống)
        Destroy(gameObject);
    }
}
