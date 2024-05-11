using TMPro;
using UniRx;
using UnityEngine;
public class TextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public static ReactiveProperty<int> Score = new ReactiveProperty<int>();
    private void Start()
    {
        Score.Subscribe(x =>
        {
            scoreText.text = $"Score : {x}";
        });
    }
}
