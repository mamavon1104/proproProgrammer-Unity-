using UniRx;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public ReactiveProperty<CardData> CardData = new ReactiveProperty<CardData>(null);
}
