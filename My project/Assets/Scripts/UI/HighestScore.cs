using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public ScoreDisplay scoreDisplay;

    void Start()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        highScoreText.text = scoreDisplay.GetHighScore().ToString();
    }
}
