using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickPause : MonoBehaviour
{ 
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
