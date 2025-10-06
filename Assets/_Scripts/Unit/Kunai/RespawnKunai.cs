using System.Collections;
using UnityEngine;

public class RespawnKunai : MonoBehaviour
{
    [SerializeField] private GameObject kunaiGroupPrefab;
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private int totalKunai;
    [SerializeField] private int destroyedKunai;
    [SerializeField] private int currentLevel;

    [SerializeField] private KunaiController controller;
    [SerializeField] private SkillModuleManager skillModuleManager;
    [SerializeField] private int skillModuleIndex; //index trong SkillModuleManager.skillModules

    private void Awake()
    {
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

        // Spawn group mới làm con của KunaiUnit
        GameObject newGroup = Instantiate(kunaiGroupPrefab, kunaiUnitParent);


        SkillModule module = skillModuleManager.skillModules[skillModuleIndex];

        module.modulePrefabs[currentLevel - 1] = newGroup; //mảng trong mảng

        module.skillParent = newGroup.transform.parent.gameObject;

        controller.ResetCoroutines();

        // Hủy group cũ
        Destroy(gameObject);
    }
}
