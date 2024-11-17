using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "CastleData", menuName = "Mamavon Packs/Unity/CastleData")]
public class CastleLevelData : ScriptableObject
{
    public int _level;
    public float costGenerationInterval;
    public int _lvUprequiredCost;
}
public class Castle : MonoBehaviour, IDamageable
{
    [SerializeField] int _currentHP;
    [SerializeField] Faction m_faction;

    Subject<int> damageSubject = new Subject<int>();
    AsyncSubject<Unit> deathSubject;

    #region IDamageableインターフェース継承
    public Faction Faction => m_faction;

    public IObservable<int> OnDamaged => damageSubject;
    public IObservable<Unit> OnDeath => deathSubject ?? (deathSubject = new AsyncSubject<Unit>());

    public bool IsDead => _currentHP <= 0;
    public int CalculateDamage(int damage) => damage; //城には防御力が無いとして。
    #endregion
    public void TakeDamage(int damage)
    {
        _currentHP -= damage;
        damageSubject.OnNext(_currentHP);

        if (!IsDead)
            return;

        deathSubject.OnNext(Unit.Default);
        deathSubject.OnCompleted();
    }
}

public class Castle2 : MonoBehaviour
{
    [SerializeField] CastleLevelManager _manager;
    [SerializeField] ReactiveProperty<CastleLevelData> castleLevelData;
    [SerializeField] int castleLevel = 1;

    [SerializeField] ReactiveProperty<int> cost = new ReactiveProperty<int>(0);

    private void Start()
    {
        castleLevelData.Value = _manager.LevelDatas[0];
        Observable.Interval(TimeSpan.FromSeconds(castleLevelData.Value.costGenerationInterval)).
            Subscribe(_ =>
            {
                cost.Value++;
            });
    }
    //private Dictionary<int, float> levelAndIntervalTable = new Dictionary<int, float>()
    //{
    //    {1, 3.0f},
    //    {2, 2.2f},
    //    {3, 1.4f},
    //    {4, 0.6f},
    //};
}
public class CastleLevelManager : MonoBehaviour
{
    [SerializeField] CastleLevelData[] m_levelDatas;

    public CastleLevelData[] LevelDatas
    {
        get => m_levelDatas;
    }


    public void LevelUpCastle()
    {

    }
}
public class UIManagerRTS : MonoBehaviour
{
    [SerializeField] Button entity1, entity2, entity3;
    [SerializeField] Button levelUpButton;
    [SerializeField] CastleLevelManager levelManager;

    void Start()
    {
        levelUpButton.OnClickAsObservable().Subscribe(_ =>
        {
            levelManager.LevelUpCastle();
        }).AddTo(this);
    }
}