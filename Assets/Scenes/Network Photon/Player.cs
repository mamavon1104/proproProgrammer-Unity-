using Fusion;
using Projectiles;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour　//モノビヘイビアーではない。 PhotonではないMonobehaviour
{
    [Networked] private TickTimer delay { get; set; }
    [Networked] public bool spawnedProjectile { get; set; }
    [SerializeField] private PhysicsBall _prefabPhysxBall;
    private NetworkCharacterController _networkCharaController;
    private Vector3 _forward, back;

    [SerializeField] private WeaponBase myWeapon;
    //[SerializeField] private Ball _prefabBall;

    /// <summary>
    /// NetworkBehaviour の変更を検出し、その変更を返す。
    /// 変更を検出する NetworkBehaviour インスタンス。
    /// CopyChanges:.
    /// 検出された変更をコピーするかどうか。デフォルトはtrue。
    /// 返り値:
    /// NetworkBehaviour によって検出された変更の列挙可能。
    /// </summary>
    private ChangeDetector _changeDetector;
    public Material _material;
    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
    }
    private void Awake()
    {
        _networkCharaController = GetComponent<NetworkCharacterController>();
        _forward = transform.forward;
        back = -transform.forward;
        _material = GetComponentInChildren<MeshRenderer>().material;
    }
    private void Update()
    {
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.R))
        {
            RPC_SendMessage("Hey Mate!");
        }
    }

    /// <summary>
    /// RpcSources.InputAuthority：オブジェクトの入力権限を持つクライアントの
    /// みがRPCを送信できます
    /// RpcTargets.StateAuthority：オブジェクトの状態権限を持つホストがRPCを
    /// 受信します
    /// RpcHostMode.SourceIsHostPlayer：ホストはクライアントとしてRPCを送信
    /// します（ホストはサーバーとクライアントを兼ねているため、どちらの役割とし
    /// てRPCを送信するのかを指定する必要があります）
    /// </summary>
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_SendMessage(string message, RpcInfo info = default)
    {
        RPC_RelayMessage(message, info.Source);
    }
    private TMP_Text _messages;
    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode =
    RpcHostMode.SourceIsServer)]
    public void RPC_RelayMessage(string message, PlayerRef messageSource)
    {
        if (_messages == null)
            _messages = FindObjectOfType<TMP_Text>();
        if (messageSource == Runner.LocalPlayer)
        {
            message = $"You said: {message}\n";
        }
        else
        {
            message = $"Some other player said: {message}\n";
        }
        _messages.text += message;
    }

    /// <summary>
    /// FixedUpdateNetwork()内で色を変えるようにするとキー入力をする自分
    /// は即座に色が変わりますが、他のネットワーク上にいるプレイヤーと同期がズレることがあります。
    /// それぞれが「変更があったときのみ」情報を送るので、ネットワーク情報量がすくなくてすみます。
    /// ポスト シミュレーション フレーム レンダリング コールバックは
    /// すべてのシミュレーションが終了した後に実行されます。
    /// Fusion が物理を処理するときに Unity の Update の代わりに使用します。
    /// </summary>
    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                //changeDirector.DetectChanges(このインスタンス)で変更されたとき、色々foreachで取得し、
                //名前がspawnedProjectileだった場合に色を変えます
                case nameof(spawnedProjectile):
                    _material.color = Color.white;
                    break;
            }
        }
        _material.color = Color.Lerp(_material.color, Color.blue, Time.deltaTime);
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

                //Runner.Spawn(
                //    _prefabBall,                                                      //prefabボールを作成
                //    transform.position + _forward, Quaternion.LookRotation(_forward), //前方を向いていて、自分の前方に
                //    Object.InputAuthority, (runner, o) =>                             //ネットワークオブジェクトのInitを呼び出し(初期化)
                //    {
                //        //ラグ保証の為にWeaponBaseからFireを付ける。
                //        o.GetComponent<WeaponBase>().Fire();
                //    }
                //);
                //Runner.Spawn(
                //    _prefabBall,                                                      //prefabボールを作成
                //    transform.position + back, Quaternion.LookRotation(back), //前方を向いていて、自分の前方に
                //    Object.InputAuthority, (runner, o) =>                             //ネットワークオブジェクトのInitを呼び出し(初期化)
                //    {
                //        //ラグ保証の為にWeaponBaseからFireを付ける。
                //        o.GetComponent<WeaponBase>().Fire();
                //    }
                //);
                myWeapon.Fire();


                spawnedProjectile = !spawnedProjectile;
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
                spawnedProjectile = !spawnedProjectile;
            }
        }
    }
}