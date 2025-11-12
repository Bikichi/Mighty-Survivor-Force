using UnityEngine;
using System.Collections;

public class AutoScaleDestroy : MonoBehaviour
{
    public void StartScale(float duration)
    {
        StartCoroutine(ScaleAndDestroy(duration));
    }

    private IEnumerator ScaleAndDestroy(float duration)
    {
        Transform t = transform;
        Vector3 initialScale = t.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            t.localScale = Vector3.Lerp(initialScale, Vector3.zero, progress);
            yield return null;
        }

        Destroy(gameObject);
    }
}
