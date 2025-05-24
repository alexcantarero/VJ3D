using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScoreDisplay scoreDisplay;
    public GameObject panel;

    public GameObject MenuPausa;
    public GameObject WinMenu;
    public GameObject LooseMenu;

    public bool starSpawned = false;
    public bool isPaused = false;

    private int activeBalls = 1;

    void Start()
    {
        //MenuPausa = GameObject.Find("MenuPausa");

        if (scoreDisplay == null)
            scoreDisplay = FindObjectOfType<ScoreDisplay>();
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
        LooseGame();
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

    public void LooseGame()
    {
        Debug.Log("Loose Game!");
        LooseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void WinGame()
    {
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
            LooseGame();
        }
        if (Input.GetKeyDown(KeyCode.W) && !WinMenu.activeSelf)
        {
            WinGame();
        }
    }
}
