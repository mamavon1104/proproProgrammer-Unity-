using UnityEngine;

public class GetCoinScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("a");
            PlayerController.coinNum.Value += 1;
        }
    }
}
