using UnityEngine;

public class WeaponOrbit : MonoBehaviour
{
    public Transform player;

    [Header("Cài đặt cơ bản")]
    [SerializeField] private string relatedSkillName; // ví dụ: "Kunai" hoặc "Sawblade"
    [SerializeField] private float baseOrbitSpeed = 300f;
    [SerializeField] private float speedIncreasePercent = 10f;
    [SerializeField] private Vector3 followOffset = Vector3.zero;

    [Header("Runtime")]
    [SerializeField] private PlayerSkillManager skillManager;

    public bool canOrbit = true; //biến kiểm soát quay
    [SerializeField] private float orbitSpeed;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        orbitSpeed = baseOrbitSpeed;
    }

    private void Start()
    {
        if (skillManager != null)
            skillManager.OnSkillLevelUp += UpdateOrbitSpeed;
    }

    private void OnDisable()
    {
        if (skillManager != null)
            skillManager.OnSkillLevelUp -= UpdateOrbitSpeed;
    }
    private void UpdateOrbitSpeed(ScriptableObject skill, int skillLevel)
    {
        if (skill.name != relatedSkillName) return;

        float multiplier = 1 + (speedIncreasePercent / 100f);
        orbitSpeed = baseOrbitSpeed * Mathf.Pow(multiplier, skillLevel - 1);
    }

    private void Update()
    {
        FollowPlayer();

        if (canOrbit)
            Orbit();
    }

    private void Orbit()
    {
        if (player != null)
            transform.RotateAround(player.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }

    private void FollowPlayer()
    {
        if (player != null)
            transform.position = player.position + followOffset;
    }
}
