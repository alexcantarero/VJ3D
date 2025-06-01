using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // Único TMP usado
    public ScoreDisplay scoreDisplay;     // Script que lleva el score

    private int currentHighScore;
    private bool hasShownNewHighScore = false;

    void Start()
    {
        currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + currentHighScore;
    }

    void Update()
    {
        int currentScore = scoreDisplay.GetCurrentScore();

        if (!hasShownNewHighScore && currentScore > currentHighScore)
        {
            highScoreText.text = "NEW HIGH SCORE: " + currentScore;
            hasShownNewHighScore = true;
        }
    }
}
