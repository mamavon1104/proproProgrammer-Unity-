using Cysharp.Threading.Tasks;
using UnityEngine;

public class CoinRandom : MonoBehaviour
{
    [SerializeField] GameObject CoinPrefab;
    private async void Awake()
    {
        for (int i = 0; i < 200; i++)
        {
            await UniTask.Yield();
            var coin = Instantiate(CoinPrefab);
            var x = Random.Range(-8.47f, -1.22f);
            var y = -3.4f;
            var z = Random.Range(-20.03f, -8.28f);
            coin.transform.position = new Vector3(x, y, z);
        }
    }
}
