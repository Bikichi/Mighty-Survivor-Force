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
        orbit.canOrbit = false;
        canFly = true;
    }

    public void ResetState()
    {
        orbit.canOrbit = true;
        canFly = false;
    }

    public void ResetCoroutines()
    {
        StopAllCoroutines();
        StartCoroutine(HandleKunaiState());
    }
}
