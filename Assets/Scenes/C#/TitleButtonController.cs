using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleButtonController : MonoBehaviour
{
    [SerializeField] Button startButton, continueButton, rankingButton;
    [SerializeField] GameObject rankingTextPrefab;
    [SerializeField] GameObject rankingPreantImage;
    [SerializeField] Transform rankingContent;
    private void Start()
    {
        startButton.OnClickAsObservable().Subscribe(async _ =>
        {
            if (PlayerController.coinNum.Value != 30)
            {
                var rankingText = Instantiate(rankingTextPrefab);
                DontDestroyOnLoad(rankingText);
                rankingText.transform.parent = rankingContent;
                PlayerController.coinNum.Value = 30;
            }
            await UniTask.WaitForSeconds(1f);
            GameStart();
        });

        continueButton.OnClickAsObservable().Subscribe(_ =>
        {
            GameStart();
        });
        rankingButton.OnClickAsObservable().Subscribe(_ =>
        {
            rankingPreantImage.SetActive(true);
        });
    }
    private void GameStart()
    {
        SceneManager.LoadScene("CoinPusher");
    }
}
