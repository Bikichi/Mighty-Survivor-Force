using UnityEngine;

public class WeaponOrbit : MonoBehaviour
{
    public Transform player;
    public float orbitSpeed = 50f;
    [SerializeField] private Vector3 followOffset = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Orbit();
        FollowPlayer();
    }

    public void Orbit()
    {
        if (player != null)
        {
            transform.RotateAround(player.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }

    public void FollowPlayer() 
    {
        transform.position = player.position + followOffset;
    }
}