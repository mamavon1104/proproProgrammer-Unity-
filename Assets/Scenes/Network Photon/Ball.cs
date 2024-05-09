using Fusion;

public class Ball : NetworkBehaviour
{
    [Networked] private TickTimer life { get; set; }
    public void Init()
    {
        //photon経由で時間を図りたいためTickTimerで作っている。
        life = TickTimer.CreateFromSeconds(Runner, 5.0f);
    }
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner)) //ライフが減ると(5秒間経ってしまうと)　デスポーンさせる。
            Runner.Despawn(Object);
        else
            transform.position += 5 * transform.forward * Runner.DeltaTime;
        //Time.deltaTimeだとクライアントの時間になってしまうが、Runnner.DeltaTimeを使用することでサーバー(photon)上のdeltaTimeで同期
    }
}