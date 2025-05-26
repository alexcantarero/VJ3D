using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIButtonSelector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public float selectedScale = 1.2f;
    public Vector2 normalScale = new Vector2(2.24f, 1f);

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(normalScale.x, normalScale.y, 1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down detected");

        // Escalar a un valor fijo al presionar
        rectTransform.localScale = new Vector3(
            normalScale.x * selectedScale,
            normalScale.y * selectedScale,
            1f
        );
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Pointer Up detected");

        // Volver al tamaño normal al soltar
        rectTransform.localScale = new Vector3(normalScale.x, normalScale.y, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Pointer Click detected");
        Time.timeScale = 1f;

        string selectedName = gameObject.name;

        if (selectedName == "MenuButton")
        {
            SceneManager.LoadScene("LevelsMenu");
        }
        else if (selectedName == "PlayAgainButton")
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (selectedName == "Lev1")
        {
            SceneManager.LoadScene("Level1");
        }
        else if (selectedName == "Lev2")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (selectedName == "Lev3")
        {
            SceneManager.LoadScene("Level3");
        }
        else if (selectedName == "Lev4")
        {
            SceneManager.LoadScene("Level4");
        }
        else if (selectedName == "Lev5")
        {
            SceneManager.LoadScene("Level5");
        }
        else if (selectedName == "PlayButton")
        {
            SceneManager.LoadScene("LevelsMenu");
        }
        else if (selectedName == "InstructionsButton") //
        {
            SceneManager.LoadScene("Instructions");
        }
        else if (selectedName == "OptionsButton") //
        {
            SceneManager.LoadScene("Options");
        }
        else if (selectedName == "CreditsButton")
        {
            SceneManager.LoadScene("Credits");
        }
        else if (selectedName == "BackButton")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (selectedName == "NextLevButton")
        {
            // Load the next level in the build settings
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            // Check if the next scene index is within bounds
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.LogWarning("No more levels available to load.");
            }
        }
        else if (selectedName == "ExitButton")
        {
            Application.Quit();
        }
        else
        {
            Debug.LogWarning("Button not recognized: " + selectedName);
        }
    }
}
