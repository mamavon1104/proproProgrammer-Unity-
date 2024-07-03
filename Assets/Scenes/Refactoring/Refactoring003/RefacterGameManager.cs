using UnityEngine;

public class RefacterGameManger : MonoBehaviour
{
    #region �A�W���C���J���A�E�H�[�^�[�z�[���J���ɂ��āB
    // �A�W���C���J���ƌ��������l�����f�����R�[�h�����J�����@������B
    // ���̌�ǉ�����悤�ɂȂ�����݌v�̕ύX�ɂȂ遨���t�@�N�^�����O�ɂȂ�B
    // �A�W���C���J���Ƌt�ɃE�H�[�^�[�z�[���J���ƌ�����������A
    // �ŏ��Ƀo�J�ł������̂����ƕ������Ă�̂�
    // �ėp�����x���x�������v���O�������쐬���Ă����A�ƌ����J�����@�B�炵���B
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
        //���^FindObjectsOfType����ByType�֏����
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