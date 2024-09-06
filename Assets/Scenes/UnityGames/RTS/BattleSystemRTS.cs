using Cysharp.Threading.Tasks;
using Mamavon.Funcs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BattleSystemRTS : MonoBehaviour
{
    [SerializeField] EntityData _entityData;
    [SerializeField] SphereCollider _attackCol;
    [SerializeField] BattleSystemRTS _enemy = default;
    private void Start()
    {
        if (_entityData == null)
            "“ü‚Á‚Ä‚Ü‚¹‚ñentityData".DebuglogError();

        if (_attackCol == null)
            SetChildCollider();

        _attackCol.OnTriggerEnterAsObservable().Subscribe(obj =>
        {
            "debug".Debuglog();
            if (!obj.transform.TryGetComponent<BattleSystemRTS>(out var colliderEnemy))
                return;

            if (colliderEnemy._entityData.type == _entityData.type)
                return;

            _enemy = colliderEnemy;
        });
        _attackCol.OnTriggerExitAsObservable().Subscribe(obj =>
        {
            if (!obj.transform.TryGetComponent<BattleSystemRTS>(out var colliderEnemy))
                return;

            if (colliderEnemy != _enemy)
                return;

            _enemy = null;
        });

        this.UpdateAsObservable().Where(_ => _enemy != null)
            .Subscribe(async _ =>
            {
                //    await UniTask.WaitForSeconds(_entityData._attackSpan);

                (_enemy._entityData._hp -= 10).Debuglog();
            });
    }

    public void SetChildCollider()
    {
        transform.GetChild(0).TryGetComponent<SphereCollider>(out _attackCol);
    }
}
