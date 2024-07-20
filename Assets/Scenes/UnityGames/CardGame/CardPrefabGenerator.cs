using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardPrefabGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private CardData[] _cardData;

    private void Awake()
    {
        var myT = transform;
        var cardDictionary = new List<CardHandler>();
        foreach (var cardData in _cardData)
        {
            var card = Instantiate(_cardPrefab, myT);
            card.name = cardData.name;
            if (!card.TryGetComponent<CardHandler>(out var handler))
                handler = card.AddComponent<CardHandler>();

            handler.CardData = cardData;

            cardDictionary.Add(handler);
        }

        if (!myT.TryGetComponent<CardSorter>(out var cardSorter))
            cardSorter = myT.AddComponent<CardSorter>();

        cardSorter.SetCardDic(cardDictionary);
    }
}
