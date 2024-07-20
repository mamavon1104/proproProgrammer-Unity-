using Cysharp.Threading.Tasks;
using Mamavon.Funcs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHandler : MonoBehaviour, IDragHandler, IDropHandler
{
    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] CardData cardData;
    public CardData CardData
    {
        set { cardData = value; }
        get { return cardData; }
    }

    [SerializeField] CardSlot myCardSlot; //現在入ってたりするハンドラー。
    public CardSlot CardSlot
    {
        get { return myCardSlot; }
        private set { myCardSlot = value; }
    }

    private Vector2 myPos;
    private Transform myT;

    private async void Start()
    {
        await UniTask.WaitUntil(() => cardData != null);
        myText.text = $"HP : {cardData.hp}\nCost : {cardData.cost}\nCount : {cardData.cardsCount}";

        myT = transform;
        ResetPosition();
    }

    public async void ResetPosition()
    {
        //1フレーム待たないとGridLayoutGroupで待たないといけない。大変お怒り、なんだこの仕様ふざけんなよ
        await UniTask.Yield();
        myPos = myT.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        myT.position = eventData.position;
    }
    public void OnDrop(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var hit in results)
        {
            Transform otherT = hit.gameObject.transform;
            if (otherT.TryGetComponent<CardSlot>(out var slot))
            {
                otherT.Debuglog();

                if (slot.CardData.Value != null && slot.CardData.Value != CardData)
                    break;

                if (CardSlot != null && CardSlot != slot)
                    break;

                myT.position = otherT.position;
                CardSlot = slot;
                CardSlot.CardData.Value = cardData;
                return;
            }
        }

        myT.position = myPos;

        myT.Debuglog();

        if (CardSlot.Debuglog(TextColor.Green) == null)
            return;

        CardSlot.CardData.Value = null;
        CardSlot = null;
    }
}

