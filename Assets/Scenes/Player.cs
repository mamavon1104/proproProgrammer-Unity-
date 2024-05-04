using Fusion;

public class Player : NetworkBehaviour　//モノビヘイビアーではない。 PhotonではないMonobehaviour
{
    private NetworkCharacterController _cc;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork() //ネットワーク経由でFixedUpdateが呼び出される
    {
        //Network dataをoutで渡して
        if (GetInput(out NetworkInputData data))
        {
            //まわりまわって取得したdataのベクトルを正規化
            data.direction.Normalize();
            _cc.Move(5f * data.direction * Runner.DeltaTime); //ネットワークのdeltaTime
        }
    }

}
