using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScoreDisplay scoreDisplay;

    void Start()
    {
        if (scoreDisplay == null)
            scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");

        if (scoreDisplay.IsNewHighScore())
        {
            GameObject panel = GameObject.FindGameObjectWithTag("HighScoreUI");
            if (panel != null)
            {
                panel.SetActive(true);
                Debug.Log("High score panel activated.");
            }
        }
    }
}
