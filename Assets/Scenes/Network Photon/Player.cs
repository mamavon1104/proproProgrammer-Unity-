using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour　//モノビヘイビアーではない。 PhotonではないMonobehaviour
{
    [Networked] private TickTimer delay { get; set; }
    [SerializeField] private Ball _prefabBall;
    [SerializeField] private PhysicsBall _prefabPhysxBall;
    private NetworkCharacterController _networkCharaController;
    private Vector3 _forward, right, left, back;

    private void Awake()
    {
        _networkCharaController = GetComponent<NetworkCharacterController>();
        _forward = transform.forward;
        back = -transform.forward;
    }

    public override void FixedUpdateNetwork()
    {
        //Network dataをoutで渡して
        if (GetInput(out NetworkInputData data))
        {
            //まわりまわって取得したdataのベクトルを正規化　ネットワークから来たデータは基本違うと思ってチート対策
            data.direction.Normalize();

            _networkCharaController.Move(5 * data.direction * Runner.DeltaTime); //runnnerのDeltaTimeとか

            if (data.direction.sqrMagnitude > 0) //direcの平方根が0以上だったら 正規化ベクトルあげる。
            {
                _forward = data.direction;
                back = -data.direction;
            }

            if (!delay.ExpiredOrNotRunning(Runner))
                return;

            if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON1))//もしマウスが押されているなら
            {
                delay = TickTimer.CreateFromSeconds(Runner, 0.1f); //0.5秒間createを連打を無視する、
                Runner.Spawn(
                    _prefabBall,                                                      //prefabボールを作成
                    transform.position + _forward, Quaternion.LookRotation(_forward), //前方を向いていて、自分の前方に
                    Object.InputAuthority, (runner, o) =>                             //ネットワークオブジェクトのInitを呼び出し(初期化)
                    {
                        // Initialize the Ball before synchronizing it
                        o.GetComponent<Ball>().Init();
                    }
                );
                Runner.Spawn(
                    _prefabBall,                                                      //prefabボールを作成
                    transform.position + back, Quaternion.LookRotation(back), //前方を向いていて、自分の前方に
                    Object.InputAuthority, (runner, o) =>                             //ネットワークオブジェクトのInitを呼び出し(初期化)
                    {
                        // Initialize the Ball before synchronizing it
                        o.GetComponent<Ball>().Init();
                    }
                );
            }
            else if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON2))
            {
                delay = TickTimer.CreateFromSeconds(Runner, 0.1f);
                Runner.Spawn(
                    _prefabPhysxBall,
                    transform.position + _forward,
                    Quaternion.LookRotation(_forward),
                    Object.InputAuthority, (runner, o) =>
                    {
                        o.GetComponent<PhysicsBall>().Init(25 * _forward);
                    }
                );
            }
        }
    }
}