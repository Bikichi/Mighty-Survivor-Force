using UnityEngine;

public class BossMultiShoot : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Transform[] spawnPoints;

    public GameObject bulletPrefab;
  

    public void FireMulti()
    {

        foreach (Transform spawn in spawnPoints)
        {
            if (spawn == null) continue;

            GameObject bullet = Instantiate(bulletPrefab, spawn.position, spawn.rotation);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireMulti();
        }
    }
}
