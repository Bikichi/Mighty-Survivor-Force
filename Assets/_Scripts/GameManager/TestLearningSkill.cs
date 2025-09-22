using UnityEngine;

public class TestLearningSkill : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private PlayerPassiveSkillManager skillManager;
    [SerializeField] private PassiveSkillScriptableObject testSkill;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            skillManager.LearnPassiveSkill(testSkill);

            Debug.Log($"Current {testSkill.skillName} Level = {skillManager.GetSkillLevel(testSkill)}");
            Debug.Log($"Player Damage = {PlayerStats.Instance.damage}, HP = {PlayerStats.Instance.maxHP}");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            playerGameObject.AddComponent<RegenSkill>();
            Debug.Log("Learning Regen Skill!");
        }


        if (Input.GetKeyDown(KeyCode.R)) //reset skill
        {
            skillManager.ResetSkills();
            var regen = playerGameObject.GetComponent<RegenSkill>();
            if (regen != null) Destroy(regen);
            Debug.Log("All skills reset! Stats back to base.");
        }
    }
}
