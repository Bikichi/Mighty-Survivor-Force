using UnityEngine;

public class SawBladeOrbit : MonoBehaviour
{
    public Transform player;
    public float orbitSpeed = 50f;

    void Update()
    {
        if (player != null)
        {
            transform.RotateAround(player.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}