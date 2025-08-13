using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBalls : PlayerBullet
{
    [Header("Ice Field Settings")]
    [SerializeField] private GameObject iceFieldPrefab;
    [SerializeField] private float iceFieldDuration;

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
            SpawnIceField(col.transform.position);
        }
    }

    private void SpawnIceField(Vector3 position)
    {
        if (iceFieldPrefab != null)
        {
            GameObject iceField = Instantiate(iceFieldPrefab, position, Quaternion.identity);

            Destroy(iceField, iceFieldDuration);
        }
    }
}
