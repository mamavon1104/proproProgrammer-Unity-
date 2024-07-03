using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject resultRoot;

    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject clearText;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI resultScore;
    [SerializeField] TextMeshProUGUI resultTime;
    [SerializeField] TextMeshProUGUI resultLife;
    [SerializeField] TextMeshProUGUI resultTotalScore;
    [SerializeField] TextMeshProUGUI highScoreUI;

    private void Start()
    {
        gameOverText.SetActive(false);
        clearText.SetActive(false);
        resultRoot.SetActive(false);
    }

    public void SetScoreText(int score = 0)
    {
        scoreText.text = "Score: " + score;
    }
    public void SetHighScoreText(int highScore)
    {
        highScoreUI.text = "High Score: " + highScore;
    }
    public void SetTimeText(float time)
    {
        highScoreUI.text = "High Score: " + time;
    }
    public void ShowGameOverUI()
    {
        gameOverText.SetActive(true);
    }
    public void ShowGameClearUI()
    {
        clearText.SetActive(true);
    }
    public void PrintResultUIs(int score, float leftTime, int ballCount, int toralScore)
    {
        resultRoot.SetActive(true);
        resultScore.text = "Score: " + score;
        resultTime.text = "Time: " + Mathf.RoundToInt(leftTime) + "x 100 = " + Mathf.RoundToInt(leftTime) * 100;
        resultLife.text = "Life: " + ballCount + "x 500 = " + ballCount * 500;

        resultTotalScore.text = "Total Score: " + toralScore;
    }
}
