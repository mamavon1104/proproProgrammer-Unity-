using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [SerializeField] GameObject hitEffectPrefab;
    Transform myT;
    bool myIsAnimating = true;
    private void Start()
    {
        myT = transform;
        this.OnMouseDownAsObservable().
            ThrottleFirst(TimeSpan.FromMilliseconds(2000)) //2•b–³Ž‹
            .Subscribe(_ =>
            {
                if (WhackAMoleTextManager.time.Value <= 0)
                    return;

                Instantiate(hitEffectPrefab, myT.position, Quaternion.identity);
                myIsAnimating = false;
                ++WhackAMoleTextManager.score.Value;
                Debug.Log("aaaa");
            });
    }
    private async void OnEnable()
    {
        myIsAnimating = true;
        await UniTask.WaitUntil(() => myT != null);

        var moleMoveSequence = DOTween.Sequence()
        .Append(myT.DOMoveY(0, WhackAMoleManager.moleSpawnAndMoveTime).SetEase(Ease.OutSine))
        .AppendInterval(0.3f)
        .SetLoops(2, LoopType.Yoyo)
        .OnComplete(() => gameObject.SetActive(false)); //’@‚©‚ê‚È‚¯‚ê‚Î‚±‚±‚Å’âŽ~‚È‚Í‚¸

        await UniTask.WaitUntil(() => !myIsAnimating); //’@‚©‚ê‚Äbool‚ª•Ï‚í‚Á‚½‚ç‰º‚Ö
        moleMoveSequence.Pause();

        var moleReturnSequence = DOTween.Sequence()
        .Append(myT.DOMoveY(WhackAMoleManager.moleYPos, 1f).SetEase(Ease.OutSine))
        .OnComplete(() => gameObject.SetActive(false));
    }
}
