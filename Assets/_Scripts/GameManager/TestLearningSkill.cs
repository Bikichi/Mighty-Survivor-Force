using UnityEngine;

public class TestLearningSkill : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private PlayerSkillManager skillManager;

    [Header("Test Passive Skills")]
    [SerializeField] private PassiveSkillScriptableObject testPassiveHPSkill;
    [SerializeField] private PassiveSkillScriptableObject testPassiveDamageSkill;
    [SerializeField] private PassiveSkillScriptableObject testPassiveShootCooldownSkill;
    [SerializeField] private PassiveSkillScriptableObject testPassiveAttackRangedSkill;
    [SerializeField] private PassiveSkillScriptableObject testPassiveMoveSpeedSkill;
    [SerializeField] private PassiveSkillScriptableObject testPassiveDodgeSkill;
    [SerializeField] private PassiveSkillScriptableObject testPassiveCritSkill;
    [SerializeField] private PassiveSkillScriptableObject testPassiveCritMultiplierSkill;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            skillManager.LearnSkill(testPassiveHPSkill);
            Debug.Log($"Máu tối đa = {PlayerStats.Instance.maxHP}");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            skillManager.LearnSkill(testPassiveDamageSkill);
            Debug.Log($"Sát thương cơ bản = {PlayerStats.Instance.baseDamage}");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            skillManager.LearnSkill(testPassiveShootCooldownSkill);
            Debug.Log($"Thời gian hồi bắn = {PlayerStats.Instance.baseShootCooldown}");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            skillManager.LearnSkill(testPassiveAttackRangedSkill);
            Debug.Log($"Tầm đánh = {PlayerStats.Instance.baseAttackRange}");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            skillManager.LearnSkill(testPassiveMoveSpeedSkill);
            Debug.Log($"Tốc độ di chuyển = {PlayerStats.Instance.baseMoveSpeed}");
        }

        // Dodge
        if (Input.GetKeyDown(KeyCode.O))
        {
            skillManager.LearnSkill(testPassiveDodgeSkill);
            Debug.Log($"Tỉ lệ né tránh = {PlayerStats.Instance.baseDodgeChance}%");
        }

        // Crit
        if (Input.GetKeyDown(KeyCode.P))
        {
            skillManager.LearnSkill(testPassiveCritSkill);
            Debug.Log($"Tỉ lệ chí mạng = {PlayerStats.Instance.baseCritChance}%");
        }

        // Crit Multiplier
        if (Input.GetKeyDown(KeyCode.L))
        {
            skillManager.LearnSkill(testPassiveCritMultiplierSkill);
            Debug.Log($"Sát thương chí mạng x {PlayerStats.Instance.baseCritMultiplier}");
        }

        // Test học Active Skill: Regen
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerGameObject.AddComponent<RegenSkill>();
            Debug.Log("Học kỹ năng hồi máu chủ động (Regen)!");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            FindObjectOfType<SkillSelectionManager>().GetRandomSkillChoices();
            FindObjectOfType<SkillSelectionUIController>().ShowSkillChoices();
        }
    }
}
