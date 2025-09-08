using UnityEngine;
using System.Collections;

public class KunaiController : MonoBehaviour
{
    [SerializeField] private WeaponOrbit orbit;
    [SerializeField] private float delayBeforeFly;

    public bool canFly = false;

    private void Awake()
    {
        if (orbit == null)
            orbit = GetComponent<WeaponOrbit>();
    }

    private void Start()
    {
        StartCoroutine(HandleKunaiState());
    }

    private IEnumerator HandleKunaiState()
    {
        yield return new WaitForSeconds(delayBeforeFly);

        if (orbit != null)
            orbit.enabled = false;

        canFly = true;
    }
    public void ResetState()
    {
        // reset trạng thái
        canFly = false;
        if (orbit != null)
            orbit.enabled = true;
    }

    public void ResetCoroutines()
    {
        // chạy lại coroutine
        StopAllCoroutines();
        StartCoroutine(HandleKunaiState());
    }

}
