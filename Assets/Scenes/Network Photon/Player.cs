using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour�@//���m�r�w�C�r�A�[�ł͂Ȃ��B Photon�ł͂Ȃ�Monobehaviour
{
    [Networked] private TickTimer delay { get; set; }
    [SerializeField] private Ball _prefabBall;
    private NetworkCharacterController _networkCharaController;
    private Vector3 _forward, right, left, back;

    private void Awake()
    {
        _networkCharaController = GetComponent<NetworkCharacterController>();
        _forward = transform.forward;
        right = transform.right;
        back = -transform.forward;
        left = -transform.right;
    }

    public override void FixedUpdateNetwork()
    {
        //Network data��out�œn����
        if (GetInput(out NetworkInputData data))
        {
            //�܂��܂���Ď擾����data�̃x�N�g���𐳋K���@�l�b�g���[�N���痈���f�[�^�͊�{�Ⴄ�Ǝv���ă`�[�g�΍�
            data.direction.Normalize();

            _networkCharaController.Move(5 * data.direction * Runner.DeltaTime); //runnner��DeltaTime�Ƃ�

            if (data.direction.sqrMagnitude > 0) //direc�̕�������0�ȏゾ������ ���K���x�N�g��������B
                _forward = data.direction;

            if (!delay.ExpiredOrNotRunning(Runner))
                return;

            if ((data.buttons & NetworkInputData.MOUSEBUTTON1) != 0)//�����}�E�X��������Ă���Ȃ�
            {
                delay = TickTimer.CreateFromSeconds(Runner, 0.5f); //0.5�b��create��A�ł𖳎�����A
                Runner.Spawn(
                    _prefabBall,                                                      //prefab�{�[�����쐬
                    transform.position + _forward, Quaternion.LookRotation(_forward), //�O���������Ă��āA�����̑O����
                    Object.InputAuthority, (runner, o) =>                             //�l�b�g���[�N�I�u�W�F�N�g��Init���Ăяo��(������)
                    {
                        // Initialize the Ball before synchronizing it
                        o.GetComponent<Ball>().Init();
                    }
                );
                Runner.Spawn(
                    _prefabBall,                                                      //prefab�{�[�����쐬
                    transform.position + right, Quaternion.LookRotation(right), //�O���������Ă��āA�����̑O����
                    Object.InputAuthority, (runner, o) =>                             //�l�b�g���[�N�I�u�W�F�N�g��Init���Ăяo��(������)
                    {
                        // Initialize the Ball before synchronizing it
                        o.GetComponent<Ball>().Init();
                    }
                );
                Runner.Spawn(
                    _prefabBall,                                                      //prefab�{�[�����쐬
                    transform.position + left, Quaternion.LookRotation(left), //�O���������Ă��āA�����̑O����
                    Object.InputAuthority, (runner, o) =>                             //�l�b�g���[�N�I�u�W�F�N�g��Init���Ăяo��(������)
                    {
                        // Initialize the Ball before synchronizing it
                        o.GetComponent<Ball>().Init();
                    }
                );
                Runner.Spawn(
                    _prefabBall,                                                      //prefab�{�[�����쐬
                    transform.position + back, Quaternion.LookRotation(back), //�O���������Ă��āA�����̑O����
                    Object.InputAuthority, (runner, o) =>                             //�l�b�g���[�N�I�u�W�F�N�g��Init���Ăяo��(������)
                    {
                        // Initialize the Ball before synchronizing it
                        o.GetComponent<Ball>().Init();
                    }
                );
            }
        }
    }
}