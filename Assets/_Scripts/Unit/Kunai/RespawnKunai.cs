using System.Collections;
using UnityEngine;

public class RespawnKunai : MonoBehaviour
{
    [Header("Prefabs & Settings")]
    [SerializeField] private GameObject kunaiGroupPrefab;
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private int currentLevel;

    [Header("References")]
    [SerializeField] private KunaiController controller;
    [SerializeField] private SkillModuleManager skillModuleManager;
    [SerializeField] private int skillModuleIndex; // index trong SkillModuleManager.skillModules

    [SerializeField] private int totalKunai;
    [SerializeField] private int destroyedKunai;

    private void Awake()
    {
        // Load prefab tương ứng level
        string prefabPath = $"Prefabs/Unit/Kunai/KunaiLv{currentLevel}";
        kunaiGroupPrefab = Resources.Load<GameObject>(prefabPath);

        if (controller == null)
            controller = GetComponentInParent<KunaiController>();

        if (skillModuleManager == null)
            skillModuleManager = FindAnyObjectByType<SkillModuleManager>();
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
            controller.ResetState();
            StartCoroutine(RespawnKunaiGroup());
        }
    }

    private IEnumerator RespawnKunaiGroup()
    {
        yield return new WaitForSeconds(respawnDelay);

        Transform kunaiUnitParent = transform.parent;

        //spawn group mới làm con của KunaiUnit
        GameObject newGroup = Instantiate(kunaiGroupPrefab, kunaiUnitParent);

        SkillModule module = skillModuleManager.skillModules[skillModuleIndex];

        //cập nhật module prefab mới
        module.modulePrefabs[currentLevel - 1] = newGroup;

        module.skillParent = newGroup.transform.parent.gameObject;
    
        controller.ResetCoroutines();

        //Debug.LogWarning("1 - Cần thêm ObjectPooling ở đây để tránh lỗi null tham chiếu trail khi destroy kunai!");

        Destroy(gameObject); // destroy cũ an toàn
    }
}
