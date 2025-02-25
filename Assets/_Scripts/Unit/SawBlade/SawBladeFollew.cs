using UnityEngine;

public class SawBladeFollow : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = player.position; //đi theo vị trí Player
    }
}