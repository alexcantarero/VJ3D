using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScoreDisplay scoreDisplay;
    public GameObject panel;

    public GameObject MenuPausa;

    public bool starSpawned = false;
    public bool isPaused = false;


    void Start()
    {
        //MenuPausa = GameObject.Find("MenuPausa");

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

    public void PauseGame()
    {
        Time.timeScale = 0;
        MenuPausa.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        MenuPausa.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            isPaused = !isPaused;
            if(!isPaused) PauseGame();
            else ResumeGame();
        }
    }
}
