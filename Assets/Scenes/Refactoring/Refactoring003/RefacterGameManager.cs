using UnityEngine;

public class RefacterGameManger : MonoBehaviour
{
    #region アジャイル開発、ウォーターホール開発について。
    // アジャイル開発と言う何も考えず素早くコードを作る開発方法がある。
    // その後追加するようになったら設計の変更になる→リファクタリングになる。
    // アジャイル開発と逆にウォーターホール開発と言う物がある、
    // 最初にバカでかいものを作ると分かってるので
    // 汎用性がベリベリ高いプログラムを作成しておく、と言う開発方法。らしい。
    #endregion

    [SerializeField] GameObject ballPrefab;
    [SerializeField] UIManager uiManager;
    [SerializeField] int ballCount = 3;
    [SerializeField] float leftTime = 30;

    private int score = 0;
    private int highScore = 0;
    private int brokenObjectCount;
    private bool inGame = true;

    public void AddScore(int point)
    {
        score += point;
        uiManager.SetScoreText(score);
    }

    public void OnBroken()
    {
        brokenObjectCount--;
        if (brokenObjectCount <= 0)
        {
            inGame = false;
            uiManager.ShowGameClearUI();
            ShowResult();
        }
    }
    public void OnKillBall()
    {
        ballCount--;
        switch (ballCount)
        {
            case > 0:
                GameObject newBall = Instantiate(ballPrefab);
                newBall.name = ballPrefab.name;
                break;

            default:
                inGame = false;
                uiManager.ShowGameOverUI();
                break;
        }
    }

    private void ShowResult()
    {
        int toralScore = score + Mathf.RoundToInt(leftTime) * 100 + ballCount * 500;

        uiManager.PrintResultUIs(score, leftTime, ballCount, toralScore);
        if (highScore < toralScore)
        {
            highScore = toralScore;
            uiManager.SetHighScoreText(highScore);
        }
    }

    private void Start()
    {
        uiManager.SetScoreText(score);
        //旧型FindObjectsOfTypeからByTypeへ勝手に
        brokenObjectCount = FindObjectsByType<Broken>(FindObjectsSortMode.None).Length;
    }

    private void Update()
    {
        if (inGame == false)
            return;

        leftTime -= Time.deltaTime;
        uiManager.SetTimeText(Mathf.RoundToInt(leftTime));

        if (leftTime >= 0)
            return;

        inGame = false;
        uiManager.SetTimeText(0);
        uiManager.ShowGameOverUI();
    }
}