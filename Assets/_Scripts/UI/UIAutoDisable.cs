using UnityEngine;

public class UIAutoDisable : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Disable", 3.5f);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
