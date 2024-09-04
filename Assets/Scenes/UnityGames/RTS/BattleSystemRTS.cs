using Cysharp.Threading.Tasks;
using Mamavon.Funcs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class BattleSystemRTS : MonoBehaviour
{
    [SerializeField] EntityData _entityData;
    [SerializeField] SphereCollider _attackCol;
    private void Start()
    {
        if (_entityData == null)
            "“ü‚Á‚Ä‚Ü‚¹‚ñentityData".DebuglogError();

        if (_attackCol == null)
            SetChildCollider();

        TryGetComponent<NavMeshAgent>(out var nav);
        _entityData._movementSpeed.Subscribe(speed =>
        {
            nav.speed = speed;
        });
        _entityData._attackDistance.Subscribe(distance =>
        {
            _attackCol.radius = distance;
        });

        _attackCol.OnCollisionEnterAsObservable().Subscribe(async obj =>
        {
            if (!obj.transform.TryGetComponent<BattleSystemRTS>(out var enemy))
                return;

            if (enemy._entityData.entity == _entityData.entity)
                return;

            await UniTask.WaitForSeconds(_entityData._attackSpan);
        });
        _attackCol.OnCollisionExitAsObservable().Subscribe(_ =>
        {

        });
    }

    public void SetChildCollider()
    {
        transform.GetChild(0).TryGetComponent<SphereCollider>(out _attackCol);
    }
}
