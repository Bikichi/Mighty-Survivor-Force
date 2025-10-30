using UnityEngine;

//Unity có 3 hệ tọa độ chính:
//World Space - Vị trí thật của vật thể trong thế giới 3D
//Screen Space - Tọa độ điểm hiển thị trên màn hình (pixel)
//Viewport Space - Tọa độ tương đối trong khung nhìn camera
//Screen Space và Viewport Space có gốc toạ độ đặt ở góc dưới bên trái
public class EnemyArrowController : MonoBehaviour
{
    public Camera mainCamera;       
    public GameObject arrow;           //con mũi tên đã gắn sẵn trong prefab
    public Transform player;

    private RectTransform arrowRect;

    private void Awake()
    {
        mainCamera = Camera.main;
        arrowRect = arrow.GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position); //toạ đọ theo tỉ lệ (0-1)

        //nếu enemy phía sau camera hoặc ngoài view → ẩn mũi tên
        if (viewportPos.z < 0 || (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1))
        {
            arrow.SetActive(false);
        }
        else
        {
            arrow.SetActive(true);
            UpdateArrowPositionAndRotation();
        }
    }

    private void UpdateArrowPositionAndRotation()
    {
        //vị trí enemy và player trên màn hình
        Vector3 enemyScreenPos = mainCamera.WorldToScreenPoint(transform.position); //toạ độ theo tỉ lệ màn hình (1080-1920)
        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);

        //vector từ player → enemy
        Vector3 dir = (enemyScreenPos - playerScreenPos).normalized;

        //các biên màn hình với offset để arrow không bị dính sát mép
        float xMin = 0 + 20f;
        float xMax = Screen.width - 20f;
        float yMin = 0 + 20f;
        float yMax = Screen.height - 20f;

        //tính t để giao với biên màn hình
        //công thức toán học tính toạ độ điểm trong hệ Phương trình tham số của đường thẳng
        float tX = dir.x > 0 ? (xMax - playerScreenPos.x) / dir.x :
                   dir.x < 0 ? (xMin - playerScreenPos.x) / dir.x : float.MaxValue;
        float tY = dir.y > 0 ? (yMax - playerScreenPos.y) / dir.y :
                   dir.y < 0 ? (yMin - playerScreenPos.y) / dir.y : float.MaxValue;

        //dir.x == 0 tức là song song trục y, hướng thẳng đứng không bao giờ cắt biên trái hoặc phải của màn hình
        //đặt tX = float.MaxValue khi dir.x == 0 để bỏ qua trục X khi ở bên dưới sẽ gọi Mathf.Min
        //tượng tự ngược lại với dir.y == 0 
        float t = Mathf.Min(tX, tY);

        //vị trí arrow = giao điểm với rìa màn hình
        Vector3 arrowPos = playerScreenPos + dir * t; //Phương trình tham số của đường thẳng
        arrowRect.position = arrowPos;

        //xoay arrow hướng về enemy
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Mathf.Atan2(dir.y, dir.x) tính ra góc (đơn vị radian) của vector hướng
        //Mathf.Rad2Deg quy đổi từ radian sang độ (degree) để dùng trong Quaternion
        arrowRect.rotation = Quaternion.Euler(0, 0, angle - 180); //-180 hay trừ bao nhiêu tuỳ vào spite gốc
    }
}
