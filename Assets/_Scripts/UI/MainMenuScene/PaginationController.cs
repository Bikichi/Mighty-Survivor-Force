using UnityEngine;
using UnityEngine.UI;


public class PaginationController : MonoBehaviour
{
    public Image[] dots;       //gán các dot vào đây
    public Color activeColor = Color.white;
    public Color inactiveColor; // mờ đi
    private int currentIndex = 0;

    //gọi hàm này từ MapSelection mỗi khi đổi map
    public void SetActiveIndex(int index)
    {
        currentIndex = index;
        UpdateDots();
    }

    void UpdateDots()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].color = (i == currentIndex) ? activeColor : inactiveColor;
        }
    }
}
