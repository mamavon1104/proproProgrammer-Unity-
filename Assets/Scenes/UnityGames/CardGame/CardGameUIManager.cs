using TMPro;
using UniRx;
using UnityEngine;

public class CardGameUIManager : MonoBehaviour
{
    [SerializeField] CardGameCostData costData;
    [SerializeField] CardSlotParent cardSlotParent;
    [SerializeField] TextMeshProUGUI totalCostText;
    [SerializeField] CanvasGroup confirmUIGroup;
    private void Start()
    {
        cardSlotParent.TotalCost.Subscribe(cost =>
        {
            bool canConfirm = cost < costData.TotalCostLimit; //cost10ˆÈã‚Ìê‡Ž€‚Ê

            confirmUIGroup.interactable = canConfirm;

            Color color = canConfirm ? Color.white : Color.red;
            totalCostText.color = color;
            totalCostText.text = $"TotalCost : {cost}";
        });
    }
}
