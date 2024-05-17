using Fusion;
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
}
