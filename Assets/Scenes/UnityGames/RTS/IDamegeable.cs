using System;
using UniRx;

public interface IDamageable
{
    /// <summary>
    /// �_���[�W���󂯂鏈�����������܂��B
    /// </summary>
    /// <param name="damage">�󂯂�_���[�W��</param>
    /// <returns>���ۂɓK�p���ꂽ�_���[�W��</returns>
    public void TakeDamage(int damage);

    public int CalculateDamage(int damage);
    public bool IsDead { get; }

    // �_���[�W���󂯂����̃C�x���g
    IObservable<int> OnDamaged { get; }

    // ���S���̃C�x���g
    IObservable<Unit> OnDeath { get; }

    Faction Faction { get; }
}