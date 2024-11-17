using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
/// <summary>
/// ScriptableObjectだよ。
/// </summary>
[CreateAssetMenu(fileName = "EntityData", menuName = "Mamavon Packs/Unity/EntityData")]
public class EntityData : ScriptableObject
{
    public int _initialHp;
    public int _attack;
    [FormerlySerializedAs("_diffence")]
    public int _defence;
    public float _attackSpan;
    public ReactiveProperty<float> _movementSpeed;
    public ReactiveProperty<float> _attackDistance = new ReactiveProperty<float>(13.5f);
    public Faction factionType;

    public int RequiredCost;
}