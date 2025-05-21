using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject winMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    public GameObject buttonSelectorWin;
    public GameObject buttonSelectorGameOver;
    public GameObject buttonSelectorPause;

    void Start()
    {
        // Deactivate all menus and selectors at start
        winMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);

        buttonSelectorWin.SetActive(false);
        buttonSelectorGameOver.SetActive(false);
        buttonSelectorPause.SetActive(false);
    }

    // Example functions to activate only the one you need
    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
        buttonSelectorWin.SetActive(true);
    }

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        buttonSelectorGameOver.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        buttonSelectorPause.SetActive(true);
    }

    public void HideAllMenus()
    {
        winMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);

        buttonSelectorWin.SetActive(false);
        buttonSelectorGameOver.SetActive(false);
        buttonSelectorPause.SetActive(false);
    }
}
