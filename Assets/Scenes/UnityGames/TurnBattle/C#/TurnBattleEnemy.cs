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
        Observable.EveryUpdate()        //update‚ðì¬
            .Select(_ => IsDead())      //isDead‚ÌŒÄ‚Ño‚µ
            .DistinctUntilChanged()     //‚à‚µ‚³‚Á‚«‚Æ’l‚ª•Ï‰»‚µ‚Ä‚¢‚Ä
            .Where(isDead => isDead == true)    //true‚È‚çAŽ€‚ñ‚Å‚¢‚é‚ñ‚¾‚Á‚½‚ç
            .Take(1)                    //1‰ñ
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