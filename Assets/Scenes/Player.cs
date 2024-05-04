using Fusion;

public class Player : NetworkBehaviour�@//���m�r�w�C�r�A�[�ł͂Ȃ��B Photon�ł͂Ȃ�Monobehaviour
{
    private NetworkCharacterController _cc;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork() //�l�b�g���[�N�o�R��FixedUpdate���Ăяo�����
    {
        //Network data��out�œn����
        if (GetInput(out NetworkInputData data))
        {
            //�܂��܂���Ď擾����data�̃x�N�g���𐳋K��
            data.direction.Normalize();
            _cc.Move(5f * data.direction * Runner.DeltaTime); //�l�b�g���[�N��deltaTime
        }
    }

}
