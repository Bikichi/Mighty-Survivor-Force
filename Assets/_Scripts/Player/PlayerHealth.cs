using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity    
{
    protected override void Die()
    {
        Debug.Log("Player Die!!!");
        base.Die();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.ENEMYBULLET_TAG)) //nếu đối tượng này va chạm với đối tượng có tag thì thực thi
        {
            FireDragonBullet fireDragonBullet = col.GetComponent<FireDragonBullet>();
            TakeDamage(fireDragonBullet.damageBullet);
            Destroy(col.gameObject); ; //hủy viên đạn
        }
    }
}
