using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIButtonSelector : MonoBehaviour
{
    public List<RectTransform> panels;  // Arrastra los paneles aquí en el Inspector
    public float selectedScale = 1.2f;
    public float normalScale = 1f;

    private int currentIndex = 0;

    void Start()
    {
        HighlightSelectedPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % panels.Count;
            HighlightSelectedPanel();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + panels.Count) % panels.Count;
            HighlightSelectedPanel();
        }
        else if (Input.GetKeyDown(KeyCode.Return)) 
        {
            ActivateSelectedPanel();
        }
    }

    void HighlightSelectedPanel()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == currentIndex)
            {
                panels[i].localScale = Vector3.one * selectedScale;
            }
            else
            {
                panels[i].localScale = Vector3.one * normalScale;
            }
        }
    }

    void ActivateSelectedPanel()
    {
        string selectedName = panels[currentIndex].gameObject.name;

        if (selectedName == "MenuButton") 
        {
            SceneManager.LoadScene("MainMenu"); 
        }
        
        Debug.Log("Activado: " + selectedName);
    }
}
