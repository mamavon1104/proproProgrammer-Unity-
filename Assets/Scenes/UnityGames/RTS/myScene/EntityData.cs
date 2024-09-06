using UniRx;
using UnityEngine;
/// <summary>
/// ScriptableObjectだよ。
/// </summary>
[CreateAssetMenu(fileName = "EntityData", menuName = "Mamavon Packs/ScriptableObject/EntityData")]
public class EntityData : ScriptableObject
{
    public int _hp;
    public int _attack;
    public int _diffence;
    public float _attackSpan;
    public ReactiveProperty<float> _movementSpeed;
    public ReactiveProperty<float> _attackDistance = new ReactiveProperty<float>(13.5f);
    public EntityType type;
    public enum EntityType
    {
        Team,
        Enemy,
        Other,
    }

    [ContextMenu("実行テスト")]
    private void TestFunc()
    {

    }
}