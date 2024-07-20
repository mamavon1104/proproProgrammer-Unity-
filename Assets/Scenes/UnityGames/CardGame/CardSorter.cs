using Cysharp.Threading.Tasks;
using Mamavon.Funcs;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public enum CardSortOption
{
    HP,
    Cost,
    Count
}
public class CardSorter : MonoBehaviour
{
    public ReactiveProperty<CardSortOption> _sortOption = new ReactiveProperty<CardSortOption>(CardSortOption.Cost);

    private List<CardHandler> _cardsHandlerList;
    [SerializeField] private List<Transform> cardsTrans = new List<Transform>();

    /// <summary>
    /// ソートのファンクションを返します
    /// </summary>
    /// <returns>Transform[]が返されます。</returns>
    private List<Transform> GetSortAction()
    {
        return _sortOption.Value switch
        {
            CardSortOption.Cost => _cardsHandlerList.OrderBy(card => card.CardData.cost).Select(kv => kv.transform).ToList(),

            CardSortOption.HP => _cardsHandlerList.OrderBy(card => card.CardData.hp).Select(kv => kv.transform).ToList(),

            CardSortOption.Count => _cardsHandlerList.OrderBy(card => card.CardData.cardsCount).Select(kv => kv.transform).ToList(),

            _ => _cardsHandlerList.OrderBy(card => card.CardData.cost).Select(kv => kv.transform).ToList(),
        };
    }

    private async void Start()
    {
        await UniTask.WaitUntil(() => _cardsHandlerList.Count != 0);
        _sortOption.Subscribe(_ =>
        {
            cardsTrans = GetSortAction();

            for (int i = 0; i < cardsTrans.Count; i++)
            {
                cardsTrans[i].SetSiblingIndex(i);
                _cardsHandlerList[i].ResetPosition();
            }
        });
    }
    public void ChangeSortOption()
    {
        _sortOption.Value = GetNextCardData();

        CardSortOption GetNextCardData()
        {
            int optionLength = System.Enum.GetValues(typeof(CardSortOption)).Length;
            return (CardSortOption)(((int)_sortOption.Value + 1) % optionLength).Debuglog();
        }
    }
    public void SetCardDic(List<CardHandler> generatorCardDic)
    {
        _cardsHandlerList = generatorCardDic;
    }


}