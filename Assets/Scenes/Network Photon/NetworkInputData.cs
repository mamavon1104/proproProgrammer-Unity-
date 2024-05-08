using Fusion;
using UnityEngine;

//MonoBehaviourを使用していない
public struct NetworkInputData : INetworkInput
{
    public Vector3 direction;　//どっちのベクトルが取れているのかを考える。
}