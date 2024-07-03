using Mamavon.Funcs;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnBattleCharacter : MonoBehaviour
{
    [SerializeField] private int m_maxHP = 50, m_maxSP = 1;
    [SerializeField] public ReactiveProperty<int> m_currentHP, m_currentSP;

    [SerializeField] private bool skillUsed = false;
    public bool SkillUsed
    {
        set
        {
            m_currentSP.Value--;
            skillUsed = value;
        }
    }

    [SerializeField] private int m_attackDamage = 10;
    private TurnBattleCharacter _mytarget;

    private void Awake()
    {
        m_currentHP.Value = m_maxHP;
        m_currentSP.Value = m_maxSP;
    }
    public bool IsDead()
    {
        return m_currentHP.Value <= 0;
    }
    public void SelectTarget(TurnBattleCharacter chara)
    {
        $"{_mytarget = chara}を選択..".Debuglog();
    }
    protected void Attack()
    {
        if (IsDead())
        {
            $"もう...{gameObject}は死んだんだ...動けないんすわ；；".Debuglog(TextColor.Black);
            return;
        }
        $"{_mytarget}に嫌がらせの様な{m_attackDamage}ダメージを与える...!".Debuglog();
        _mytarget.TakeDamage(m_attackDamage, this);
    }
    public void TakeDamage(int damage, TurnBattleCharacter attackedChara)
    {
        if (skillUsed && Random.Range(0, 101) <= 80)
        {
            $"{attackedChara}からの攻撃をこいつカウンターをしやがった!".Debuglog();
            attackedChara.TakeDamage(damage * 3, this); //強すぎだろ3倍80%は！期待値が😡
            skillUsed = false;
            return;
        }
        else
            $"{gameObject}は{damage}ダメージを受けた、{attackedChara}は大喜びしている！\n 残りHP : {m_currentHP.Value -= damage}".Debuglog();

        if (m_currentHP.Value > 0)
            return;

        m_currentHP.Value = 0;
        $"{gameObject}君、HPが{m_currentHP.Value}になっちゃった！死亡！下手くそ！".Debuglog();

    }
    //こっちにやってあげれば敵の回復もこれ使えるし、味方に僧侶と書いたらこういう所にアクセスすればいいよね
    public void CharaHeal(int healPower)
    {
        m_currentHP.Value += healPower;

        if (m_currentHP.Value > m_maxHP)
            m_currentHP.Value = m_maxHP;

        $"{gameObject}は{healPower}HPだけ回復した！ \n 試合途中に甘えて回復してんじゃねーよ！！現在hpは{m_currentHP.Value}HP！".Debuglog();
    }
}