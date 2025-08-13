using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBalls : PlayerBullet
{
    [Header("Fire Field Settings")]
    [SerializeField] private GameObject fireFieldPrefab;
    [SerializeField] private float fireFieldDuration;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);

        if (col.CompareTag(Const.ENEMY_TAG))
        {
            SpawnFireField(col.transform.position);
        }
    }

    private void SpawnFireField(Vector3 position)
    {
        if (fireFieldPrefab != null)
        {
            GameObject fireField = Instantiate(fireFieldPrefab, position, Quaternion.identity);

            Destroy(fireField, fireFieldDuration);
        }
    }
}
