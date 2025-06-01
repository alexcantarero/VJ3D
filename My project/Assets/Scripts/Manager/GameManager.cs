using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreDisplay scoreDisplay;
    public GameObject HighScoreUI;

    public GameObject MenuPausa;
    public GameObject WinMenu;
    public GameObject LooseMenu;

    public GameObject invisibleWall; // GodMode

    public PaddleController pC;
    private bool isGodModeActive = false; 

    public bool starSpawned = false;
    public bool isPaused = false;

    private int activeBalls = 1;
    private int totalBlocks;
    private bool EnemyDefeated = false;

    public AudioManager audioManager;

    void Start()
    {
        //MenuPausa = GameObject.Find("MenuPausa");

        if (scoreDisplay == null)
            scoreDisplay = FindObjectOfType<ScoreDisplay>();

        if (invisibleWall != null)
        {
            invisibleWall.SetActive(false);
        }

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        pC = GameObject.Find("Player").GetComponent<PaddleController>();
        if(pC == null)
        {
            Debug.LogError("PaddleController not found in GameManager.");
        }
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
        Debug.Log("Total blocks: " + totalBlocks);
    }

    public void UnregisterBlock()
    {
        totalBlocks--;
        Debug.Log("Total blocks: " + totalBlocks);

        if (totalBlocks <= 0 && EnemyDefeated)
        {
            Debug.Log("EnemyDefeated bool in GameMAnager:" + EnemyDefeated);
            WinGame();
        }
    }

    public void EnemyDead()
    {
        if (!EnemyDefeated)
        {
            EnemyDefeated = true;
        }

        if (EnemyDefeated && totalBlocks <= 0)
        {
            EnemyDefeated = true;
            Debug.Log("Enemy defeated!");
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
        audioManager.PauseMusic();
        MenuPausa.SetActive(true);
    }

    public void ResumeGame() //Despausar el juego
    {
        Time.timeScale = 1;
        audioManager.ResumeMusic();
        MenuPausa.SetActive(false);
    }

    public void GameOver()
    {
        audioManager.PlayMusic(audioManager.gameOverMusic);
        checkHighScore();
        Debug.Log("Loose Game!");
        Time.timeScale = 0;
        LooseMenu.SetActive(true);
    }

    public void WinGame()
    {
        audioManager.PlayMusic(audioManager.gameWinMusic);
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


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Level1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Level2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Level3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("Level4");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene("Level5");
        }
        else if (Input.GetKeyDown(KeyCode.L)) 
        {
            pC.SpawnTwoShells();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            pC.TurnShellRed();
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            pC.TurnShellGreen();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            pC.AugmentPaddleX();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            pC.ShrinkPaddleX();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(pC.ShootBulletBill());
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            pC.StickyPaddle();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            pC.SpeedUpShells();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            pC.SlowShells();
        }

    }
}
