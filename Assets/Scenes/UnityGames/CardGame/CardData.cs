using System;
using UnityEngine;
using Random = UnityEngine.Random;
[Serializable]
[CreateAssetMenu(menuName = "Taninon/Card Data", fileName = "CardData.asset")]
public class CardData : ScriptableObject
{
    public int hp;
    public int cost;
    public int cardsCount;

    // �ň�1�R�X�g12hp�Ƃ������������o���邯�ǎ蓮��
    private void OnEnable()
    {
        hp = Random.Range(1, 12);
        cost = Random.Range(1, 7);
        cardsCount = Random.Range(1, 256);
    }
}
