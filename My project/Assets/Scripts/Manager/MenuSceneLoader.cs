using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadMenuScene());
    }

    IEnumerator LoadMenuScene()
    {
        // Inicia la carga de la escena
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PlayMenus", LoadSceneMode.Additive);

        // Espera hasta que la escena esté completamente cargada
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Aquí ya puedes acceder a los objetos dentro de la escena PlayMenus
        Debug.Log("Escena PlayMenus cargada completamente.");

        // Si necesitas acceder a MenuManager desde aquí:
        MenuManager mm = FindObjectOfType<MenuManager>();
        if (mm != null)
        {
            Debug.Log("MenuManager encontrado.");
        }
        else
        {
            Debug.LogError("MenuManager no encontrado.");
        }
    }
}
