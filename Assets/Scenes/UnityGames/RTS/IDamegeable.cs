using System;
using UniRx;

public interface IDamageable
{
    /// <summary>
    /// ダメージを受ける処理を実装します。
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    /// <returns>実際に適用されたダメージ量</returns>
    public void TakeDamage(int damage);

    public int CalculateDamage(int damage);
    public bool IsDead { get; }

    // ダメージを受けた時のイベント
    IObservable<int> OnDamaged { get; }

    // 死亡時のイベント
    IObservable<Unit> OnDeath { get; }

    Faction Faction { get; }
}