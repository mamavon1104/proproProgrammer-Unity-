using UnityEngine;

public class GetCoinScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
            PlayerController.coinNum.Value += 1;
    }
}
