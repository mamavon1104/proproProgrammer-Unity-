using Mamavon.Funcs;
using UniRx;
using UnityEngine;

public class TurnBattlePlayer : TurnBattleCharacter
{
    [SerializeField] private readonly int skillChance = 80;
    [SerializeField] private itemData itemData;
    public ReactiveProperty<int> itemCount = new ReactiveProperty<int>(3);

    public void UseSkill()
    {
        if (m_currentSP.Value <= 0)
        {
            $"スキルの使用回数上限に達しました！ダメー。".Debuglog();
            return;
        }

        SkillUsed = true;
        IsFinishTurn();
    }
    public void StartAttack()
    {
        Attack();
        IsFinishTurn();
    }
    public void UseItem()
    {
        if (itemCount.Value > 0)
        {
            $"薬草を使うようなずるい{gameObject}君！".Debuglog();
            CharaHeal(itemData.itemAmount);
            itemCount.Value--;
            IsFinishTurn();
        }
        else
            "アイテム足りないよ～；；".Debuglog();
    }
    public void IsFinishTurn()
    {
        TurnBattleManager.Instance.currentButtleState.Value = BattleState.EnemyTurn;
    }
}

