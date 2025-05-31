using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameButtonSelector : MonoBehaviour, IPointerClickHandler
{
    public GameManager GameManager;
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string selectedName = gameObject.name;

        switch (selectedName)
        {
            case "MenuButton":
                // Load the game scene
                SceneManager.LoadScene("MainMenu");
                break;
            case "PlayAgainButton":
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.name);
                break;
            case "ResumeButton":
                GameManager.ResumeGame();
                break;
            case "NextLevButton":
                // Load the next level scene
                Scene escenaActual = SceneManager.GetActiveScene();
                if (escenaActual.name == "Level1")
                {
                    SceneManager.LoadScene("Level2");
                }
                else if (escenaActual.name == "Level2")
                {
                    SceneManager.LoadScene("Level3");
                }
                else if (escenaActual.name == "Level3")
                {
                    SceneManager.LoadScene("Level4");
                }
                else if (escenaActual.name == "Level4")
                {
                    SceneManager.LoadScene("Level5");
                }
                else if (escenaActual.name == "Level5")
                {
                    SceneManager.LoadScene("MainMenu");
                }
                break;




        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
