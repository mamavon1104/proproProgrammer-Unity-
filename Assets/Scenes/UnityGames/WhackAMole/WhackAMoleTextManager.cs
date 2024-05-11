using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
public class WhackAMoleTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText, timeText;
    public static ReactiveProperty<float> time = new ReactiveProperty<float>(30);
    public static ReactiveProperty<int> score = new ReactiveProperty<int>();
    private void Start()
    {
        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (time.Value > 0)
                time.Value -= Time.deltaTime;
            else
                time.Value = 0;
        });

        time.Subscribe(t =>
        {
            timeText.text = $"{t.ToString("F2")} : Time";
        });
        score.Subscribe(s =>
        {
            scoreText.text = $"Score: {s}";
        });
    }
}
