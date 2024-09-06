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
    /// FixedUpdateNetwork()内で色を変えるようにするとキー入力をする自分
    /// は即座に色が変わりますが、他のネットワーク上にいるプレイヤーと同期がズレることがあります。
    /// それぞれが「変更があったときのみ」情報を送るので、ネッワークでの情報量がすくなくてすみます。
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
