using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class ShowPlayerNameTMP : MonoBehaviour
{
    public TMP_Text playerNameText;

    private async void Start()
    {
        if (playerNameText == null)
            return;

        // Đợi cho tới khi Authentication chắc chắn đã đăng nhập
        await WaitForSignedIn();

        // Đặt tên ngẫu nhiên nếu chưa có
        await SetRandomPlayerName();

        // Cập nhật UI
        UpdateNameText();
    }

    private async Task WaitForSignedIn()
    {
        // Nếu chưa signed-in → đợi
        while (AuthenticationService.Instance == null ||
               !AuthenticationService.Instance.IsSignedIn)
        {
            await Task.Yield();
        }
    }

    private async Task SetRandomPlayerName()
    {
        if (string.IsNullOrEmpty(AuthenticationService.Instance.PlayerName))
        {
            string randomName = "Player#" + Random.Range(1000, 9999);
            await AuthenticationService.Instance.UpdatePlayerNameAsync(randomName);
            Debug.Log("Tên ngẫu nhiên mới: " + randomName);
        }
    }

    private void UpdateNameText()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            playerNameText.text = "Player";
            return;
        }

        playerNameText.text = !string.IsNullOrEmpty(AuthenticationService.Instance.PlayerName)
            ? AuthenticationService.Instance.PlayerName
            : AuthenticationService.Instance.PlayerId;
    }
}
