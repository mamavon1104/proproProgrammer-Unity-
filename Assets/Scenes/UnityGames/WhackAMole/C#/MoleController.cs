using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [SerializeField] float moleExposeTime = 0.3f;
    [SerializeField] GameObject hitEffectPrefab;

    Transform myT;
    bool myIsAnimating = true;
    GameObject myEffect = null;

    private void Start()
    {
        myT = transform;
        this.OnMouseDownAsObservable().
            ThrottleFirst(TimeSpan.FromMilliseconds(2000)) //2•b–³‹
            .Subscribe(_ =>
            {
                if (WhackAMoleValueManager.time.Value <= 0)
                    return;

                myEffect = Instantiate(hitEffectPrefab, myT.position, Quaternion.identity);
                myIsAnimating = false;
                ++WhackAMoleValueManager.score.Value;
                Debug.Log("aaaa");
            });
    }
    private async void OnEnable()
    {
        myIsAnimating = true;
        await UniTask.WaitUntil(() => myT != null);

        var moleMoveSequence = DOTween.Sequence()
        .Append(myT.DOMoveY(0, MoleCreater.moleSpawnAndMoveTime).SetEase(Ease.OutSine))
        .AppendInterval(moleExposeTime)
        .SetLoops(2, LoopType.Yoyo)
        .OnComplete(() => Destroy(gameObject)); //’@‚©‚ê‚È‚¯‚ê‚Î‚±‚±‚Å’â~‚È‚Í‚¸

        await UniTask.WaitUntil(() => !myIsAnimating); //’@‚©‚ê‚Äbool‚ª•Ï‚í‚Á‚½‚ç‰º‚Ö
        moleMoveSequence.Pause();

        var moleReturnSequence = DOTween.Sequence()
        .Append(myT.DOMoveY(MoleCreater.moleYPos, 1f).SetEase(Ease.OutSine))
        .OnComplete(() =>
        {
            Destroy(gameObject);

            //effect‚Ìƒ‹[ƒv‚ğ‰ğ‚¢‚Ä‚à‚¸‚Á‚Æ”½‰f‚³‚ê‘±‚¯‚é‚©‚ç‚í‚´‚í‚´•Ï”‚ÉŠi”[‚µ‚ÄÁ‚·‚µ‚©‚È‚©‚Á‚½B
            Destroy(myEffect);
        });
    }
}
