using Fusion;
using UnityEngine;

//MonoBehaviour���g�p���Ă��Ȃ�
/// <summary>
/// ����struct(�l�^)�AINetWorkInput���p�����Ă���̂Ńf�[�^�̂����̎��Ɏg�p�\��struct�ɁB
/// data = NetworkInputData�ȂǃC���X�^���X�����Ă����āA�����̒��Ƀf�[�^�𖄂ߍ���ŕԂ��Ă�����
/// </summary>
public struct NetworkInputData : INetworkInput
{
    //���t���[���n������Aint��bool����bit�������Ȃ菭�Ȃ�byte���g�p�B
    public const byte MOUSEBUTTON1 = 0x01;
    public const byte MOUSEBUTTON2 = 0x02;
    public NetworkButtons buttons;
    public Vector3 direction;�@//�ǂ����̃x�N�g�������Ă���̂����l����B
}