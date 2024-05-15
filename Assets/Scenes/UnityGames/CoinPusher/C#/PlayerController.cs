using UniRx;
using UniRx.Triggers;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform coinParent;
    public static ReactiveProperty<int> coinNum = new ReactiveProperty<int>(30);

    private void Start()
    {
        //var ct = new CancellationTokenSource();
        this.UpdateAsObservable()
         .Subscribe(_ =>
         {
             if (Input.GetKeyDown(KeyCode.Space) && coinNum.Value > 0)
             {
                 --coinNum.Value;
                 var coin = Instantiate(coinPrefab, coinParent);
                 var z = Random.Range(-20.48f, -8.71f);
                 coin.transform.position = new Vector3(-7.6f, -1f, z);
             }
         });
    }
    [AddComponentMenu("コインペアレントを取得")]
    void GetCoinParent()
    {
        var parent = GameObject.FindGameObjectWithTag("CoinParent").transform;
        if (parent != null)
        {
            coinParent = parent;
        }
    }

}
