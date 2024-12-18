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
        Observable.EveryUpdate()        //updateを作成
            .Select(_ => IsDead())      //isDeadの呼び出し
            .DistinctUntilChanged()     //もしさっきと値が変化していて
            .Where(isDead => isDead == true)    //trueなら、死んでいるんだったら
            .Take(1)                    //1回
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