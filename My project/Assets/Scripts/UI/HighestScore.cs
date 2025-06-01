using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighestScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public ScoreDisplay scoreDisplay;

    void Start()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        highScoreText.text = scoreDisplay.GetHighScore().ToString();
    }

    public void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            int score = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = score.ToString();
        }
    }

}
