
using UnityEngine;

public class Broken : MonoBehaviour
{
    [SerializeField] private RefacterGameManger gameManager;
    public int point = 100;
    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<RefacterGameManger>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Destroy(gameObject, 0.2f);
            gameManager.AddScore(point);
            gameManager.OnBroken();
        }
    }
}
