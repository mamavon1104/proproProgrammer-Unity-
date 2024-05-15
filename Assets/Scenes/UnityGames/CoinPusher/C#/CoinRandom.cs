using Cysharp.Threading.Tasks;
using UnityEngine;

public class CoinRandom : MonoBehaviour
{
    /// <summary>
    /// xÇ∆zÇÃïù
    /// </summary>
    [SerializeField]
    Vector2 xRange = new Vector2(-8.47f, -1.22f),
            zRange = new Vector2(-20.03f, -8.28f);

    [SerializeField] int createCoinNum = 200;
    [SerializeField] GameObject CoinPrefab;
    private async void Awake()
    {
        for (int i = 0; i < createCoinNum; i++)
        {
            await UniTask.Yield();
            var coin = Instantiate(CoinPrefab);
            var x = Random.Range(xRange.x, xRange.y);
            var y = -3.4f;
            var z = Random.Range(zRange.x, zRange.y);
            coin.transform.position = new Vector3(x, y, z);
        }
    }
}
