using UnityEngine;

public class SpritTemporaryVariableSubject : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody myRig;

    private Vector3 moveVector;

    void Update()
    {
        moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * CalculateMovementSpeed();

        myRig.AddForce(moveVector.x, 0, moveVector.z, ForceMode.Impulse);
    }

    private float CalculateMovementSpeed()
    {
        return Time.deltaTime * speed;
    }
}
