using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class TurnBattleEnemy : TurnBattleCharacter
{
    [SerializeField] Button myAttackedButton;
    public TextMeshProUGUI hpText;
    private void Start()
    {
        Observable.EveryUpdate()        //update���쐬
            .Select(_ => IsDead())      //isDead�̌Ăяo��
            .DistinctUntilChanged()     //�����������ƒl���ω����Ă���
            .Where(isDead => isDead == true)    //true�Ȃ�A����ł���񂾂�����
            .Take(1)                    //1��
            .Subscribe(_ =>
            {
                hpText.gameObject.SetActive(false);
                myAttackedButton.gameObject.SetActive(false);
                gameObject.SetActive(false);
            });
    }

    public void Attack(TurnBattleCharacter player)
    {
        SelectTarget(player);
        Attack();
    }
}