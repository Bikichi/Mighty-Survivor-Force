using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target; // Transform của Player
    public Camera cam;
    public Vector3 offset = new Vector3(0, 0, 0); // độ cao so với player

    void Update()
    {
        if (target == null || cam == null) return;

        Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        transform.position = screenPos;
    }
}
    