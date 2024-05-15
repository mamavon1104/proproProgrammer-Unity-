using DG.Tweening;
using UnityEngine;

public class CoinPushBox : MonoBehaviour
{
    Transform myT;
    private void Start()
    {
        myT = transform;
        Sequence coinPushSequence = DOTween.Sequence()
        .AppendInterval(0.2f)
        .Append(myT.DOMoveX(-13f, 3f))
        .AppendInterval(0.2f)
        .SetLoops(-1, LoopType.Yoyo);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
            other.transform.parent = myT;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Coin"))
            other.transform.parent = null;
    }
}
