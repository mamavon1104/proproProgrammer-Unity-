using Fusion;
using UnityEngine;

//MonoBehaviourを使用していない
/// <summary>
/// 自作struct(値型)、INetWorkInputを継承しているのでデータのやり取りの時に使用可能なstructに。
/// data = NetworkInputDataなどインスタンス化してあげて、ここの中にデータを埋め込んで返してあげて
/// </summary>
public struct NetworkInputData : INetworkInput
{
    public const byte MOUSEBUTTON1 = 0x01;
    public byte buttons;
    public Vector3 direction;　//どっちのベクトルが取れているのかを考える。
}