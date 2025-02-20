using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity 
{
    [SerializeField] private GameObject _coinDrop;
    [SerializeField] private bool hasCoin;
    public float deathAnimationTime;

    public Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected override void Die()
    {
        //Debug.Log("ENEMYDIE!!!");
        base.Die();
        DisableEnemyActions();
        StartCoroutine(HandleDeath());
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathAnimationTime); // Chờ animation kết thúc

        if (hasCoin)
        {
            Instantiate(_coinDrop, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public void DisableEnemyActions()
    {
        EnemyMovement movementScript = GetComponent<EnemyMovement>();
        if (movementScript != null) movementScript.enabled = false;

        MeleeAttack meleeAttack = GetComponentInChildren<MeleeAttack>();
        if (meleeAttack != null) meleeAttack.enabled = false;

        RangedAttack rangedAttack = GetComponentInChildren<RangedAttack>();
        if (rangedAttack != null) rangedAttack.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.PLAYERBULLET_TAG)) //nếu đối tượng này va chạm với đối tượng có tag thì thực thi
        {
            PlayerBullet playerBullet = col.GetComponent<PlayerBullet>();
            TakeDamage(playerBullet.damageBullet);
            Destroy(col.gameObject); ; //hủy viên đạn
        }
    }
    //public void OnEnemiesDieAnimationEnd()
    //{
    //    Destroy(gameObject);
    //}
}
