using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void ChangeScene(string nameScene) {
        SceneManager.LoadScene(nameScene);
    }
    public void QuitApp() {
        Application.Quit();
        Debug.Log("Se ha cerrado la aplicacion.");

    }

}
