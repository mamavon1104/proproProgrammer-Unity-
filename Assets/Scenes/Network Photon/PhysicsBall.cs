using Fusion;
using Mamavon.Funcs;
using UnityEngine;

public class PhysicsBall : NetworkBehaviour
{
    [SerializeField] TickTimer life { get; set; }
    public void Init(Vector3 forward)
    {
        life = TickTimer.CreateFromSeconds(Runner, 5.0f);
        GetComponent<Rigidbody>().velocity = forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (life.Expired(Runner))
            Runner.Despawn(Object);
    }
    [Networked] public bool spawnedProjectile { get; set; }
    [SerializeField] Material _material;
    [SerializeField] Color _color;
    private ChangeDetector _changeDetector;
    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
    }

    private void Awake()
    {
        _material = GetComponentInChildren<MeshRenderer>().material;
        _color = _material.color;
    }

    /// <summary>
    /// FixedUpdateNetwork()���ŐF��ς���悤�ɂ���ƃL�[���͂����鎩��
    /// �͑����ɐF���ς��܂����A���̃l�b�g���[�N��ɂ���v���C���[�Ɠ������Y���邱�Ƃ�����܂��B
    /// ���ꂼ�ꂪ�u�ύX���������Ƃ��̂݁v���𑗂�̂ŁA�l�b���[�N�ł̏��ʂ������Ȃ��Ă��݂܂��B
    /// �|�X�g �V�~�����[�V���� �t���[�� �����_�����O �R�[���o�b�N��
    /// ���ׂẴV�~�����[�V�������I��������Ɏ��s����܂��B
    /// Fusion ����������������Ƃ��� Unity �� Update �̑���Ɏg�p���܂��B
    /// </summary>
    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                //changeDirector.DetectChanges(���̃C���X�^���X)�ŕύX���ꂽ�Ƃ��A�F�Xforeach�Ŏ擾���A
                //���O��spawnedProjectile�������ꍇ�ɐF��ς��܂�
                case nameof(spawnedProjectile):
                    _material.color = Color.white;
                    break;
            }
        }
        _material.color = Color.Lerp(_material.color, _color, Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            "Player".Debuglog();
            spawnedProjectile = !spawnedProjectile;
            _color = Color.yellow;
        }
        else if (other.gameObject.CompareTag("box"))
        {
            "box".Debuglog();
            spawnedProjectile = !spawnedProjectile;
            _color = Color.green;
        }
    }
}
