using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScoreDisplay scoreDisplay;
    public GameObject HighScoreUI;

    public GameObject MenuPausa;
    public GameObject WinMenu;
    public GameObject LooseMenu;

    public GameObject invisibleWall; // GodMode
    private bool isGodModeActive = false; 

    public bool starSpawned = false;
    public bool isPaused = false;

    private int activeBalls = 1;
    private int totalBlocks;

    void Start()
    {
        //MenuPausa = GameObject.Find("MenuPausa");

        if (scoreDisplay == null)
            scoreDisplay = FindObjectOfType<ScoreDisplay>();

        if (invisibleWall != null)
        {
            invisibleWall.SetActive(false);
        }
    }

    public void RegisterBall()
    {
        activeBalls++;
        Debug.Log("Active balls: " + activeBalls);
    }

    public void UnregisterBall()
    {
        activeBalls--;

        Debug.Log("Active balls: " + activeBalls);

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
        Time.timeScale = 0;
        LooseMenu.SetActive(true);
    }

    public void WinGame()
    {
        checkHighScore();
        Debug.Log("Win Game!");
        Time.timeScale = 0;
        WinMenu.SetActive(true);
    }

    private bool checkAvailableKeys()
    {
        return !LooseMenu.activeSelf && !WinMenu.activeSelf && !MenuPausa.activeSelf;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !LooseMenu.activeSelf && !WinMenu.activeSelf) 
        {
            isPaused = !isPaused;
            Debug.Log("isPaused: " + (isPaused ? "1" : "0"));
            if (isPaused) PauseGame();
            else ResumeGame();
        }
        if (Input.GetKeyDown(KeyCode.Q) && checkAvailableKeys())
        {
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.W) && checkAvailableKeys())
        {
            WinGame();
        }

        if (invisibleWall != null)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                isGodModeActive = !isGodModeActive;
                //Debug.Log("God Mode " + (isGodModeActive ? "activated" : "deactivated"));
            }

            if (isGodModeActive)
            {
                invisibleWall.SetActive(isGodModeActive);
            }
            else
            {
                if (invisibleWall != null)
                {
                    invisibleWall.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("Invisible wall not assigned in GameManager.");
        }
        Debug.Log("Active balls: " + activeBalls);

    }
}
