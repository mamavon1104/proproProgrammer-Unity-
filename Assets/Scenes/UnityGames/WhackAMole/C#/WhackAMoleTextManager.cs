using TMPro;
using UniRx;
using UnityEngine;
public class WhackAMoleTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText, timeText;
    private void Start()
    {
        WhackAMoleValueManager.time.Subscribe(t =>
        {
            timeText.text = $"{t.ToString("F2")} : Time";
        });
        WhackAMoleValueManager.score.Subscribe(s =>
        {
            scoreText.text = $"Score: {s}";
        });
    }
}
