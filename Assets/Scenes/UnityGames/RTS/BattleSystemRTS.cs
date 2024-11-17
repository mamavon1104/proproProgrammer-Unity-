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

    #region IDamageable�C���^�[�t�F�[�X�p��
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
            "�����Ă܂���entityData".DebuglogError();

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

        //setActive��false�ɂȂ����u�Ԏ���ł��炤�B
        var isThisEnabled = this.UpdateAsObservable().Where(_ => !gameObject.activeSelf).First();

        SetTriggersActive(isThisEnabled);
        SetEntityProcces(isThisEnabled);

        OnDeath.Subscribe(_ =>
        {
            "���S".Debuglog(gameObject.ToString());
            _target = null;
            onDamagedSubject.OnCompleted();
            gameObject.SetActive(false);
        }).AddTo(this);
    }

    /// <summary>
    /// AttackCollider�ɓ�������o�����̏���������
    /// </summary>
    private void SetTriggersActive(IObservable<Unit> isThisEnabled)
    {
        _attackCol.OnTriggerEnterAsObservable().
            TakeUntil(isThisEnabled).
            Subscribe(obj =>
            {
                if (!obj.transform.TryGetComponent<IDamageable>(out var damageable).Debuglog($"{damageable}���擾"))
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
    /// �U���̎��s�Ƃ�hp�Ȃ�Entity�ɕK�v�ȓ���̐ݒ�
    /// </summary>
    private void SetEntityProcces(IObservable<Unit> isThisEnabled)
    {
        //attackSpan���o�Ŗ�����s�A����_enemy��false����Ȃ�������U���J�n ��Observable.Interval�łł���炵���Ł`
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

        //HP��0�ȉ��ɂȂ����玀��
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
