using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("PlayMenus", LoadSceneMode.Additive);
        Debug.Log("Menus cargasgod en la escena");

    }

}
