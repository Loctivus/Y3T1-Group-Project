using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMenuScript : MonoBehaviour
{
    [SerializeField]
    public string sceneToLoad;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
