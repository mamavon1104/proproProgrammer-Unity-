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
            ThrottleFirst(TimeSpan.FromMilliseconds(2000)) //2�b����
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
        .OnComplete(() => Destroy(gameObject)); //�@����Ȃ���΂����Œ�~�Ȃ͂�

        await UniTask.WaitUntil(() => !myIsAnimating); //�@�����bool���ς�����牺��
        moleMoveSequence.Pause();

        var moleReturnSequence = DOTween.Sequence()
        .Append(myT.DOMoveY(MoleCreater.moleYPos, 1f).SetEase(Ease.OutSine))
        .OnComplete(() =>
        {
            Destroy(gameObject);

            //effect�̃��[�v�������Ă������Ɣ��f���ꑱ���邩��킴�킴�ϐ��Ɋi�[���ď��������Ȃ������B
            Destroy(myEffect);
        });
    }
}
