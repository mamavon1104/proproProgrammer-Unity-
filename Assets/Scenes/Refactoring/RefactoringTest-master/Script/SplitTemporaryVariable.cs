using UnityEngine;

public class SplitTemporaryVariable : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody setRigidbody;

    private void Update()
    {
        Vector3 velocity = new Vector3();
        float velocityAxis;

        velocityAxis = Input.GetAxis("Vertical");
        velocity.y = velocityAxis;

        velocityAxis = Input.GetAxis("Horizontal");
        velocity.x = velocityAxis;

        setRigidbody.velocity = velocity * speed * Time.deltaTime;
    }
}
