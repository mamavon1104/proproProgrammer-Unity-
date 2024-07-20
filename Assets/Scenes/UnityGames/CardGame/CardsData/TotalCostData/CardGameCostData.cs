using UnityEngine;

/// <summary>
/// ScriptableObjectだよ。
/// </summary>
[CreateAssetMenu(fileName = "CardGameTotalCostData", menuName = "Taninon/CardGameTotalCost")]
public class CardGameCostData : ScriptableObject
{
    [SerializeField] int totalCostLimit = 10;
    public int TotalCostLimit
    {
        get { return totalCostLimit; }
    }
}