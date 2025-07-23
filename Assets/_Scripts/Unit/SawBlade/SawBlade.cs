using UnityEngine;

public class SawBlade : MonoBehaviour
{
    public Transform player;
    public float orbitSpeed = 50f;

    private void Start()
    {
        SawBladeFollow();   
    }

    void Update()
    {
        SawBladeOrbit();
        SawBladeFollow();
    }

    public void SawBladeOrbit()
    {
        if (player != null)
        {
            transform.RotateAround(player.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }

    public void SawBladeFollow() 
    {
        transform.position = player.position; //đi theo vị trí Player
    }
}