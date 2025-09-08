using UnityEngine;

public class DamageUIManager : Singleton<DamageUIManager>
{
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Transform uiParnet;

    public void ShowDamageUI(float damage, Collider enemyCollider, bool isCrit = false) //tham số isCrit không bắt buộc, nếu không truyền vào thì mặc định nhận giá trị false
    {
        //xác định vị trí spawn trên đầu enemy
        Vector3 spawnPosition = enemyCollider.bounds.center + new Vector3(0, enemyCollider.bounds.size.y * 0.6f, 0);

        //tạo UI từ prefab
        GameObject go = Instantiate(floatingTextPrefab, uiParnet);
        go.transform.position = spawnPosition;

        //set text và color
        var floatingText = go.GetComponent<FloatingText>();
        if (floatingText != null)
        {
            Color textColor = isCrit ? Color.red : Color.white;
            floatingText.Setup(Mathf.RoundToInt(damage).ToString(), textColor);
        }
    }
}
