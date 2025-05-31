using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIButtonSelector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public float selectedScale = 1.2f;
    public Vector2 normalScale = new Vector2(2.24f, 1f);

    private RectTransform rectTransform;


    // ------- Menús --------

    public GameObject mainMenu;
    public GameObject credits;
    public GameObject options;
    public GameObject instructions;
    public GameObject selectLevel;

    AudioManager audioManager;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(normalScale.x, normalScale.y, 1f);

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene. Please ensure it is present.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.localScale = new Vector3(
            normalScale.x * selectedScale,
            normalScale.y * selectedScale,
            1f
        );
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        rectTransform.localScale = new Vector3(normalScale.x, normalScale.y, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioManager.PlaySFX(audioManager.pressButtonSFX);
        Time.timeScale = 1f;

        string selectedName = gameObject.name;

        if (selectedName == "MenuButton")
        {
            LoadMenu(mainMenu);
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
            LoadMenu(selectLevel);
        }
        else if (selectedName == "InstructionsButton") //
        {
            LoadMenu(instructions);
        }
        else if (selectedName == "OptionsButton") //
        {
            LoadMenu(options);
        }
        else if (selectedName == "CreditsButton")
        {
            LoadMenu(credits);
        }
        else if (selectedName == "BackButton")
        {
            LoadMenu(mainMenu);
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

    private void LoadMenu(GameObject menu)
    {
        mainMenu.SetActive(false);
        credits.SetActive(false);
        options.SetActive(false);
        instructions.SetActive(false);
        selectLevel.SetActive(false);
        if (menu != null)
        {
            menu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Menu GameObject is null.");
        }
    }
}
