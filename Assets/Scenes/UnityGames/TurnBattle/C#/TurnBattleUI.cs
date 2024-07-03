using Mamavon.Funcs;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

//���̋L��������₷���A
//https://qiita.com/toRisouP/items/5365936fc14c7e7eabf9#:~:text=Model%2DView%2D(Reactive)Presenter%20%E3%83%91%E3%82%BF%E3%83%BC%E3%83%B3%EF%BC%88%E7%95%A5%E3%81%97%E3%81%A6MV,%E3%81%A8%E3%81%84%E3%81%A3%E3%81%9F%E3%82%82%E3%81%AE%E3%82%92%E6%8C%87%E3%81%97%E3%81%BE%E3%81%99%E3%80%82

public class TrunBattleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHPText, playerSPText, itemText;
    [SerializeField] TurnBattlePlayer player;
    [SerializeField] TurnBattleEnemy[] enemys;


    [SerializeField] List<CanvasGroup> m_canvasGroupList;
    [SerializeField] TextMeshProUGUI text;
    private Dictionary<CanvasGroup, Button[]> groupChildButtons = new Dictionary<CanvasGroup, Button[]>();
    private void Start()
    {
        player.itemCount.Subscribe(count =>
        {
            itemText.text = $"item : {count}";
        });
        player.m_currentHP.Subscribe(hp =>
        {
            playerHPText.text = $"playerHP : {hp}";
        });
        player.m_currentSP.Subscribe(sp =>
        {
            playerSPText.text = $"playerSP : {sp}";
        });

        foreach (var enemy in enemys)
        {
            enemy.m_currentHP.Subscribe(hp =>
            {
                enemy.hpText.text = $"HP : {hp}";
            });
        }

        ButtonSettings();
    }
    private void ButtonSettings()
    {
        //�q����CanvasGroup���擾
        foreach (Transform child in transform)
        {
            if (!child.TryGetComponent(out CanvasGroup childCanvasGroup).Debuglog(TextColor.Blue) || m_canvasGroupList.Contains(childCanvasGroup).Debuglog(TextColor.Red))
                continue;

            m_canvasGroupList.Add(childCanvasGroup);
        }

        //CanvasGroup�ŁAKey : CanvasGroup, Value : Button��Dictionary��ݒ�B
        foreach (var thisGroup in m_canvasGroupList)
        {
            Button[] buttons = thisGroup.GetComponentsInChildren<Button>();
            groupChildButtons.Add(thisGroup, buttons);
        }

        //Button�������ꂽ�Ƃ��̐ݒ�Ȃǂ��B
        foreach (var groupButtonsPair in groupChildButtons)
        {
            var parentCanvasGroup = groupButtonsPair.Key;
            foreach (var button in groupButtonsPair.Value)
            {
                button.OnClickAsObservable().Subscribe(_ =>
                {
                    "�{�^����������܂���".Debuglog(TextColor.Green);
                    CanvasGroupActiveChange(false, parentCanvasGroup); //false�Őe��canvasgroup�������Ȃ�����
                    var buttonData = button.GetComponent<ButtonData>();
                    text.text = buttonData.m_myString;
                    CanvasGroupActiveChange(true, buttonData.ActiveCanvasGroup); //true�Ŏ���canvasgroup��\�����鏈���B
                });
            }
        }
    }
    private void CanvasGroupActiveChange(bool canActive, CanvasGroup group)
    {
        group.alpha = canActive ? 1f : 0f;
        group.interactable = canActive;
        group.blocksRaycasts = canActive;
    }
}