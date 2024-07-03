using UnityEngine;

public class TestReplaceTemp : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("価格:" + GetPrice(10, 200));
    }

    private float GetPrice(int quantity, int itemPrice)
    {
        return DiscountFactor(quantity * itemPrice);
    }

    private static float DiscountFactor(int basePrice)
    {
        if (basePrice > 1000)
        {
            return basePrice * 0.95f;
        }
        else
        {
            return basePrice * 0.98f;
        }
    }
}
