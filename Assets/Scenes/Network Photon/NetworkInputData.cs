using Fusion;
using UnityEngine;

//MonoBehaviour���g�p���Ă��Ȃ�
/// <summary>
/// ����struct(�l�^)�AINetWorkInput���p�����Ă���̂Ńf�[�^�̂����̎��Ɏg�p�\��struct�ɁB
/// data = NetworkInputData�ȂǃC���X�^���X�����Ă����āA�����̒��Ƀf�[�^�𖄂ߍ���ŕԂ��Ă�����
/// </summary>
public struct NetworkInputData : INetworkInput
{
    public const byte MOUSEBUTTON1 = 0x01;
    public byte buttons;
    public Vector3 direction;�@//�ǂ����̃x�N�g�������Ă���̂����l����B
}