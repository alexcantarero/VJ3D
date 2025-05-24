using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScoreDisplay scoreDisplay;
    public GameObject HighScoreUI;

    public GameObject MenuPausa;
    public GameObject WinMenu;
    public GameObject LooseMenu;

    public bool starSpawned = false;
    public bool isPaused = false;

    private int activeBalls = 1;
    private int totalBlocks;

    void Start()
    {
        //MenuPausa = GameObject.Find("MenuPausa");

        if (scoreDisplay == null)
            scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }

    public void RegisterBall()
    {
        activeBalls++;
        //Debug.Log("Active balls: " + activeBalls);
    }

    public void UnregisterBall()
    {
        activeBalls--;

        //Debug.Log("Active balls: " + activeBalls);

        if (activeBalls <= 0)
        {
            GameOver();
        }
    }
    public void RegisterBlock()
    {
        totalBlocks++;
        //Debug.Log("Total blocks: " + totalBlocks);
    }

    public void UnregisterBlock()
    {
        totalBlocks--;
        //Debug.Log("Total blocks: " + totalBlocks);

        if (totalBlocks <= 0)
        {
            WinGame();
        }
    }

    public void checkHighScore()
    {
        Debug.Log("Game Over!");

        if (scoreDisplay.IsNewHighScore())
        {
            if (HighScoreUI != null)
            {
                HighScoreUI.SetActive(true);
                //Debug.Log("High score HighScoreUI activated.");
            }
            else
            {
                Debug.LogWarning("High score HighScoreUI not found.");
            }
        }
    }

    

    public void PauseGame()
    {
        Time.timeScale = 0;
        MenuPausa.SetActive(true);
    }

    public void ResumeGame() //Despausar el juego
    {
        Time.timeScale = 1;
        MenuPausa.SetActive(false);
    }

    public void GameOver()
    {
        checkHighScore();
        Debug.Log("Loose Game!");
        LooseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void WinGame()
    {
        checkHighScore();
        Debug.Log("Win Game!");
        WinMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            isPaused = !isPaused;
            if(!isPaused) PauseGame();
            else ResumeGame();
        }
        if (Input.GetKeyDown(KeyCode.Q) && !LooseMenu.activeSelf)
        {
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.W) && !WinMenu.activeSelf)
        {
            WinGame();
        }
    }
}
