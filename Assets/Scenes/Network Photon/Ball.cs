using Fusion;

public class Ball : NetworkBehaviour
{
    [Networked] private TickTimer life { get; set; }
    public void Init()
    {
        //photon�o�R�Ŏ��Ԃ�}�肽������TickTimer�ō���Ă���B
        life = TickTimer.CreateFromSeconds(Runner, 5.0f);
    }
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner)) //���C�t�������(5�b�Ԍo���Ă��܂���)�@�f�X�|�[��������B
            Runner.Despawn(Object);
        else
            transform.position += 5 * transform.forward * Runner.DeltaTime;
        //Time.deltaTime���ƃN���C�A���g�̎��ԂɂȂ��Ă��܂����ARunnner.DeltaTime���g�p���邱�ƂŃT�[�o�[(photon)���deltaTime�œ���
    }
}