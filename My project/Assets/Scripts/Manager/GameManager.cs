using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScoreDisplay scoreDisplay;
    public GameObject panel;


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
            if (panel != null)
            {
                panel.SetActive(true);
                Debug.Log("High score panel activated.");
            }
            else
            {
                Debug.LogWarning("High score panel not found.");
            }
        }
    }
}
