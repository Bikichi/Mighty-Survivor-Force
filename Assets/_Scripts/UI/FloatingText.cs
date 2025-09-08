using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float moveUpSpeed = 50f;
    public float lifetime = 1.2f; //tổng thời gian mà text tồn tại trên màn hình
    public float fadeDuration = 0.8f; //thời gian mờ dần, nhỏ hơn lifetime và sẽ là 1 khoảng ở cuối của lifetime

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private float timer;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        FaceCamera();
        MoveUp();
        HandleFadeAndDestroy();
    }

    public void Setup(string message, Color color)
    {
        textMesh.text = message;
        textMesh.color = color;
    }

    public void FaceCamera()
    {
        if (Camera.main == null) return;

        Vector3 direction = transform.position - Camera.main.transform.position;
        direction.y = 0; //giữ đứng thẳng, không nghiêng theo camera
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void MoveUp()
    {
        rectTransform.position += Vector3.up * moveUpSpeed * Time.deltaTime;
    }

    public void HandleFadeAndDestroy()
    {
        timer += Time.deltaTime;
        if (timer > lifetime - fadeDuration && canvasGroup != null)
        {
            float fadeStart = lifetime - fadeDuration;
            float fadeProgress = (timer - fadeStart) / fadeDuration;
            fadeProgress = Mathf.Clamp01(fadeProgress);
            canvasGroup.alpha = 1f - fadeProgress;
        }

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
