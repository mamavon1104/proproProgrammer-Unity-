using TMPro;
using UniRx;
using UnityEngine;

public class CoinNumText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gameOverText;
    private void Start()
    {
        PlayerController.coinNum.Subscribe(num =>
        {
            coinText.text = $"Coin~{num}";
            if (num <= 0)
            {
                gameOverText.text = "GAME OVER";
            }
        });
    }
}
