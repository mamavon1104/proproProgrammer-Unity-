using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeSortOptionButton : MonoBehaviour
{
    [SerializeField] CardSorter _cardSorter;
    [SerializeField] TextMeshProUGUI myText;
    private void Start()
    {
        Transform myT = transform;

        if (_cardSorter == null)
            _cardSorter = FindAnyObjectByType<CardSorter>();

        if (myText == null)
            myText = myT.GetChild(0).GetComponent<TextMeshProUGUI>();

        var button = myT.GetComponent<Button>();

        button.OnClickAsObservable().Subscribe(_ =>
        {
            _cardSorter.ChangeSortOption();
        });
        _cardSorter._sortOption.AsObservable().Subscribe(value =>
        {
            myText.text = $"Sort : {value}";
        });
    }
}
