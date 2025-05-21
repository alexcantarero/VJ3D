using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private MenuManager mm;

    void Awake()
    {
        mm = FindObjectOfType<MenuManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (mm.pauseMenu.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        mm.ShowPauseMenu();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        mm.HideAllMenus();
        Time.timeScale = 1f;
    }
}
