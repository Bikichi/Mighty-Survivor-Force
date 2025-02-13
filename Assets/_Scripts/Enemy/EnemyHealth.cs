using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity 
{
    public PlayerBullet playerBullet;
    [SerializeField] private GameObject _coinDrop;
    [SerializeField] private bool hasCoin;


    private void Update()
    {
        playerBullet = FindAnyObjectByType<PlayerBullet>();
    }
    protected override void Die()
    {
        base.Die();
        //Debug.Log("ENEMYDIE!!!");
        if (hasCoin)
        {
            Instantiate(_coinDrop, transform.position, transform.rotation);
        }
        Destroy(gameObject);
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        es.OnEnemyKilled();
    }

    //private void OnTriggerEnter(Collider col)
    //{
    //    if (col.CompareTag(Const.PLAYERBULLET_TAG)) //nếu đối tượng này va chạm với đối tượng có tag là PLAYERBULLET_TAG thì thực thi
    //    {
    //        //Debug.Log("BANG!!!");
    //        TakeDamage(playerBullet.damageBullet);
    //        Destroy(col.gameObject); ; //hủy đối tượng va chạm với đối tượng mà phương thức gắn vào
    //    }
    //}
}
