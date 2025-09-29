using UnityEngine;

public class TestLearningSkill : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private PlayerPassiveSkillManager skillManager;
    [SerializeField] private PassiveSkillScriptableObject testHPSkill;
    [SerializeField] private PassiveSkillScriptableObject testDamageSkill;
    [SerializeField] private PassiveSkillScriptableObject testShootCooldownSkill;
    [SerializeField] private PassiveSkillScriptableObject testAttackRangedSkill;
    [SerializeField] private PassiveSkillScriptableObject testMoveSpeedSkill;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            skillManager.LearnPassiveSkill(testHPSkill);
       
            Debug.Log($"MaxHP = {PlayerStats.Instance.maxHP}");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {     
            skillManager.LearnPassiveSkill(testDamageSkill);

            Debug.Log($"Player Damage = {PlayerStats.Instance.baseDamage}");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            skillManager.LearnPassiveSkill(testShootCooldownSkill);

            Debug.Log($"Shoot Cooldown = {PlayerStats.Instance.baseShootCooldown}");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            skillManager.LearnPassiveSkill(testAttackRangedSkill);

            Debug.Log($"Player Attack Ranged = {PlayerStats.Instance.baseAttackRange}");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            skillManager.LearnPassiveSkill(testMoveSpeedSkill);

            Debug.Log($"Player Move Speed = {PlayerStats.Instance.baseMoveSpeed}");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerGameObject.AddComponent<RegenSkill>();
            Debug.Log("Learning Regen Skill!");
        }
    }
}
