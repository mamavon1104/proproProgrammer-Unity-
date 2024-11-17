using Mamavon.Funcs;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BattleSystemRTS : MonoBehaviour, IDamageable
{
    [SerializeField] EntityData m_entityData;
    [SerializeField] SphereCollider _attackCol;
    [SerializeField] IDamageable _target = default;

    [SerializeField] private ReactiveProperty<int> _currentHP = new ReactiveProperty<int>();

    #region IDamageableインターフェース継承
    private Subject<int> onDamagedSubject = new Subject<int>();
    private AsyncSubject<Unit> onDeathSubject = new AsyncSubject<Unit>();
    public IObservable<int> OnDamaged => onDamagedSubject;
    public IObservable<Unit> OnDeath => onDeathSubject;
    public bool IsDead => _currentHP.Value <= 0;
    public Faction Faction => m_entityData.factionType;
    #endregion

    private void Awake()
    {
        if (m_entityData == null)
            "入ってませんentityData".DebuglogError();

        if (_attackCol == null)
            SetChildCollider();
    }

    public void TakeDamage(int damage)
    {
        _currentHP.Value -= damage;
        onDamagedSubject.OnNext(_currentHP.Value);
    }

    public int CalculateDamage(int damage)
    {
        return Mathf.Max(0, m_entityData._defence - damage);
    }

    public void SetChildCollider()
    {
        transform.GetChild(0).TryGetComponent<SphereCollider>(out _attackCol);
    }

    private void OnEnable()
    {
        GameObject gameObject = this.gameObject;

        _currentHP.Value = m_entityData._initialHp;

        //setActiveがfalseになった瞬間死んでもらう。
        var isThisEnabled = this.UpdateAsObservable().Where(_ => !gameObject.activeSelf).First();

        SetTriggersActive(isThisEnabled);
        SetEntityProcces(isThisEnabled);

        OnDeath.Subscribe(_ =>
        {
            "死亡".Debuglog(gameObject.ToString());
            _target = null;
            onDamagedSubject.OnCompleted();
            gameObject.SetActive(false);
        }).AddTo(this);
    }

    /// <summary>
    /// AttackColliderに入ったり出た時の初期化処理
    /// </summary>
    private void SetTriggersActive(IObservable<Unit> isThisEnabled)
    {
        _attackCol.OnTriggerEnterAsObservable().
            TakeUntil(isThisEnabled).
            Subscribe(obj =>
            {
                if (!obj.transform.TryGetComponent<IDamageable>(out var damageable).Debuglog($"{damageable}を取得"))
                    return;

                if (damageable.Faction == this.Faction)
                    return;

                _target = damageable;
            });

        _attackCol.OnTriggerExitAsObservable().
            TakeUntil(isThisEnabled).
            Subscribe(obj =>
            {
                if (!obj.transform.TryGetComponent<IDamageable>(out var damageable))
                    return;

                if (damageable != _target)
                    return;

                _target = null;
            });
    }
    /// <summary>
    /// 攻撃の実行とかhpなどEntityに必要な動作の設定
    /// </summary>
    private void SetEntityProcces(IObservable<Unit> isThisEnabled)
    {
        //attackSpan感覚で毎回実行、もし_enemyがfalseじゃなかったら攻撃開始 をObservable.Intervalででけるらしいで～
        Observable.Interval(TimeSpan.FromSeconds(m_entityData._attackSpan)).
            TakeUntil(isThisEnabled).
            Where(_ => _target != null).
            Subscribe(_ =>
            {
                if (!_target.IsDead)
                    _target.TakeDamage(m_entityData._attack);
                else
                    _target = null;
            }).AddTo(this);

        //HPが0以下になったら死ぬ
        _currentHP.TakeUntil(isThisEnabled).
            Where(_ => IsDead).Subscribe(_ =>
            {
                if (onDeathSubject != null && !onDeathSubject.IsCompleted)
                {
                    onDeathSubject.OnNext(Unit.Default);
                    onDeathSubject.OnCompleted();
                }
            }).AddTo(this);
    }
}
