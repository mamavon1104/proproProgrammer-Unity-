using UniRx;
using UnityEngine;

public class CardSlotParent : MonoBehaviour
{
    [SerializeField] CardSlot[] _childSlots;
    [SerializeField] int[] childCosts;
    [SerializeField] ReactiveProperty<int> totalCost = new ReactiveProperty<int>();

    public ReactiveProperty<int> TotalCost
    {
        get => totalCost;
    }

    private void Start()
    {
        if (_childSlots.Length == 0)
            SetCardSlots();

        childCosts = new int[_childSlots.Length];

        for (int i = 0; i < _childSlots.Length; i++)
        {
            int myNum = i; //myNumを付けてあげることでiが4ではなくそれぞれこのキャッシュされたNumを参照する!
            _childSlots[myNum].CardData.Subscribe(cardData =>
            {
                if (cardData != null) childCosts[myNum] = cardData.cost;
                else childCosts[myNum] = 0;

                CalculateTotalCost();
            });
        }
    }

    private void CalculateTotalCost()
    {
        int total = 0;
        foreach (var cost in childCosts)
        {
            total += cost;
        }
        totalCost.Value = total;
    }

    [ContextMenu("CardSlotの取得")]
    private void SetCardSlots()
    {
        _childSlots = GetComponentsInChildren<CardSlot>();
    }
}
