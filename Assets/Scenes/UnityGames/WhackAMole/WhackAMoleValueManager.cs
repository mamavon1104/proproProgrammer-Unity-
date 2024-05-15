using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class WhackAMoleValueManager : MonoBehaviour
{
    [SerializeField] float gameTime = 30;
    public static ReactiveProperty<float> time = new ReactiveProperty<float>(30);
    public static ReactiveProperty<int> score = new ReactiveProperty<int>();
    private void Start()
    {
        time.Value = gameTime;
        score.Value = 0;

        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (time.Value > 0)
                time.Value -= Time.deltaTime;
            else
                time.Value = 0;
        });
    }
}
